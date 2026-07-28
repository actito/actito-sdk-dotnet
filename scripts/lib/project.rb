require 'fileutils'
require_relative 'system'
require_relative 'terminal'

class CSProject
  attr_reader :component, :platform, :directory

  def initialize(component:, platform:, directory:)
    @component = component
    @platform = platform
    @directory = directory
  end

  class << self
    def all
      [
        CSProject.new(
          component: :core,
          platform: :android,
          directory: 'Actito.Android.Binding'
        ),
        CSProject.new(
          component: :core,
          platform: :ios,
          directory: 'Actito.iOS.Binding'
        ),
        CSProject.new(
          component: :assets,
          platform: :android,
          directory: 'Actito.Assets.Android.Binding'
        ),
        CSProject.new(
          component: :assets,
          platform: :ios,
          directory: 'Actito.Assets.iOS.Binding'
        ),
        CSProject.new(
          component: :geo,
          platform: :android,
          directory: 'Actito.Geo.Android.Binding'
        ),
        CSProject.new(
          component: :geo_beacons,
          platform: :android,
          directory: 'Actito.Geo.Android.Beacons'
        ),
        CSProject.new(
          component: :geo,
          platform: :ios,
          directory: 'Actito.Geo.iOS.Binding'
        ),
        CSProject.new(
          component: :in_app_messaging,
          platform: :android,
          directory: 'Actito.InAppMessaging.Android.Binding'
        ),
        CSProject.new(
          component: :in_app_messaging,
          platform: :ios,
          directory: 'Actito.InAppMessaging.iOS.Binding'
        ),
        CSProject.new(
          component: :inbox,
          platform: :android,
          directory: 'Actito.Inbox.Android.Binding'
        ),
        CSProject.new(
          component: :inbox,
          platform: :ios,
          directory: 'Actito.Inbox.iOS.Binding'
        ),
        CSProject.new(
          component: :loyalty,
          platform: :android,
          directory: 'Actito.Loyalty.Android.Binding'
        ),
        CSProject.new(
          component: :loyalty,
          platform: :ios,
          directory: 'Actito.Loyalty.iOS.Binding'
        ),
        CSProject.new(
          component: :notification_service_extension,
          platform: :ios,
          directory: 'Actito.NotificationServiceExtension.iOS.Binding'
        ),
        CSProject.new(
          component: :push,
          platform: :android,
          directory: 'Actito.Push.Android.Binding'
        ),
        CSProject.new(
          component: :push,
          platform: :ios,
          directory: 'Actito.Push.iOS.Binding'
        ),
        CSProject.new(
          component: :push_ui,
          platform: :android,
          directory: 'Actito.Push.UI.Android.Binding'
        ),
        CSProject.new(
          component: :push_ui,
          platform: :ios,
          directory: 'Actito.Push.UI.iOS.Binding'
        ),
        CSProject.new(
          component: :user_inbox,
          platform: :android,
          directory: 'Actito.UserInbox.Android.Binding'
        ),
        CSProject.new(
          component: :user_inbox,
          platform: :ios,
          directory: 'Actito.UserInbox.iOS.Binding'
        ),
      ]
    end

    def find(component, platform)
      all.find { |e| e.component == component && e.platform == platform }
    end
  end

  def name
    "#{component}/#{platform}"
  end

  def clean
    Dir.chdir(directory) do
      execute_system_command('dotnet clean')
    end
  end

  def build
    Dir.chdir(directory) do
      execute_system_command('dotnet build')
    end
  end

  def generate_binding_sources
    raise 'Cannot generate sharpie sources for non-iOS projects.' if platform != :ios

    puts "▸ Running Objective-Sharpie".blue
    sharpie_out = run_objective_sharpie

    copy_generated_api_definitions(sharpie_out)
    copy_generated_structs_and_enums(sharpie_out)
  end

  def xcframeworks
    file_contents = File.read(File.join(directory, "#{directory}.csproj"))
    file_contents.scan(/libs\\|\/(\w+.xcframework)/).flatten
  end

  def update_native_library_version(version)
    if platform == :android
      update_native_android_library_version(version)
    else
      update_native_ios_library_version(version)
    end
  end

  private def update_native_android_library_version(version)
    file = File.join(directory, "#{directory}.csproj")
    contents = File.read(file)

    contents = contents.gsub(
      /<NativeLibraryVersion>(.*)<\/NativeLibraryVersion>/,
      "<NativeLibraryVersion>#{version}</NativeLibraryVersion>"
    )

    maven_repository = "Central"
    if version.include? "-SNAPSHOT"
      maven_repository = "https://central.sonatype.com/repository/maven-snapshots/"
    end

    contents = contents.gsub(
      /<NativeLibraryMavenRepository>(.*)<\/NativeLibraryMavenRepository>/,
      "<NativeLibraryMavenRepository>#{maven_repository}</NativeLibraryMavenRepository>"
    )

    File.write(file, contents)
  end

  private def update_native_ios_library_version(version)
    bridge_project_directory = "#{directory}.Bridge"
    file = File.join(bridge_project_directory, "#{binding_scheme}.xcodeproj", 'project.pbxproj')
    contents = File.read(file)

    repository_name = "actito-sdk-ios"
    repository_url = "https://github.com/actito/actito-sdk-ios"
    if version.include? "canary"
      repository_name = "actito-sdk-ios-in-house-releases"
      repository_url = "git@github.com:actito/actito-sdk-ios-in-house-releases.git"
    end

    contents = contents.gsub(
      /XCRemoteSwiftPackageReference "actito-sdk-ios(-in-house-releases)?"/,
      "XCRemoteSwiftPackageReference \"#{repository_name}\""
    )

    contents = contents.gsub(/(Begin XCRemoteSwiftPackageReference.*RemoteSwiftPackageReference "#{repository_name}".*repositoryURL = ")[^;]+(";.*requirement = {.*kind = exactVersion;.*version = ")[^;]+(";.*\};.*End XCRemoteSwiftPackageReference)/m) do
      "#{$1}#{repository_url}#{$2}#{version}#{$3}"
    end

    File.write(file, contents)
  end

  protected def root_namespace
    file_contents = File.read(File.join(directory, "#{directory}.csproj"))
    file_contents.match(/<RootNamespace>(.+)<\/RootNamespace>/).captures[0]
  end

  def binding_scheme
    file_contents = File.read(File.join(directory, "#{directory}.csproj"))
    file_contents.match(/<SchemeName>(.+)<\/SchemeName>/).captures[0]
  end

  private def run_objective_sharpie
    command = <<~COMMAND
      sharpie bind --output=sharpie-out \
        --namespace=#{root_namespace} \
        --sdk=#{latest_sharpie_sdk} \
        --scope=Headers \
        Headers/#{binding_scheme}-Swift.h
    COMMAND

    framework = File.join(directory, 'bin', 'Debug', 'net9.0-ios', "#{directory}.resources", "#{binding_scheme}iOS.xcframework", 'ios-arm64', "#{binding_scheme}.framework")
    Dir.chdir(framework) do
      puts "▸ Running Objective-Sharpie".blue
      puts command
      execute_system_command(command)
    end

    "#{framework}/sharpie-out"
  end

  private def latest_sharpie_sdk
    _, version = capture_system_command_output(
      'xcrun --sdk iphoneos --show-sdk-version',
      stream_output: false
    )

    "iphoneos#{version.strip}"
  end

  private def depends_on_core
    core_project = CSProject.find(:core, platform)

    contents = File.read(File.join(directory, "#{directory}.csproj"))
    contents.include?(core_project.directory)
  end

  private def copy_generated_api_definitions(sharpie_out)
    filename = File.join(sharpie_out, 'ApiDefinitions.cs')
    return unless File.file?(filename)

    contents = File.read(filename)

    puts "▸ Removing scheme's default namespace".blue
    contents = contents.gsub(/using #{binding_scheme};(\n)?/, '')

    if depends_on_core
      puts "▸ Including core's namespace in imports".blue

      contents = <<~CONTENT
      using #{CSProject.find(:core, platform).root_namespace};

      #{contents}
      CONTENT
    end

    puts "▸ Applying known exceptions to generated code".blue

    # iOS ActitoAsset needs to conform to INativeObject because 'fetch' returns an
    # NSArray<ActitoAsset> and NSArray<T> requires T to be INativeObject.
    if component == :assets && platform == :ios
      contents = contents.gsub(/interface ActitoAsset$/, 'interface ActitoAsset : INativeObject')
    end

    File.write(File.join(directory, 'ApiDefinitions.cs'), contents)
  end

  private def copy_generated_structs_and_enums(sharpie_out)
    filename = File.join(sharpie_out, 'StructsAndEnums.cs')
    return unless File.file?(filename)

    FileUtils.cp(filename, directory)
  end
end
