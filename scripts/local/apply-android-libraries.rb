require 'optparse'
require 'ostruct'
require_relative '../lib/project'
require_relative '../lib/terminal'

options = OpenStruct.new

OptionParser.new do |opt|
  opt.on('-v', '--version VERSION', String, 'The native library version.') { |o| options.version = o }
end.parse!

def options.require_argument(key, message)
  raise OptionParser::MissingArgument, message if self[key].nil?
end

options.require_argument('version', 'The version is required. Please provide it by using -v or --version.')

REPO_ROOT = File.expand_path('../..', __dir__)
MAVEN_LOCAL = File.join(Dir.home, '.m2', 'repository')
ACTITO_GROUP_ID = 'com.actito'

def parse_attributes(element)
  element.scan(/([A-Za-z_:][\w:.-]*)="([^"]*)"/).to_h
end

def local_aar_path(coordinate, version)
  group_id, artifact_id = coordinate.split(':', 2)
  raise "Invalid Maven coordinate: #{coordinate}" if group_id.nil? || artifact_id.nil?

  File.join(
    MAVEN_LOCAL,
    *group_id.split('.'),
    artifact_id,
    version,
    "#{artifact_id}-#{version}.aar"
  )
end

def actito_artifact?(coordinate)
  coordinate&.start_with?("#{ACTITO_GROUP_ID}:")
end

def android_library_element(indent, aar_path, coordinate, attributes)
  bind = attributes.fetch('Bind', 'true')
  verify_dependencies = attributes.fetch('VerifyDependencies', 'false')
  attr_indent = "#{indent}        "

  <<~XML.chomp
    #{indent}<AndroidLibrary
    #{attr_indent}Include="#{aar_path}"
    #{attr_indent}JavaArtifact="#{coordinate}"
    #{attr_indent}Bind="#{bind}"
    #{attr_indent}VerifyDependencies="#{verify_dependencies}"
    #{attr_indent}JavaVersion="$(NativeLibraryVersion)" />
  XML
end

def rewrite_android_library(element, version)
  attributes = parse_attributes(element)
  coordinate = attributes['Include'] || attributes['JavaArtifact']

  return element unless actito_artifact?(coordinate)

  aar_path = local_aar_path(coordinate, version)

  unless File.file?(aar_path)
    raise <<~ERROR
      Could not find local Maven AAR.

      Artifact:
          #{coordinate}

      Version:
          #{version}

      Expected:
          #{aar_path}
    ERROR
  end

  indent = element[/\A([ \t]*)</, 1] || ''
  android_library_element(indent, aar_path, coordinate, attributes)
end

def update_project(project, version)
  file = File.join(project.directory, "#{project.directory}.csproj")
  contents = File.read(file)

  contents = contents.gsub(
    /<NativeLibraryVersion>(.*)<\/NativeLibraryVersion>/,
    "<NativeLibraryVersion>#{version}</NativeLibraryVersion>"
  )

  updated = contents.gsub(/^[ \t]*<Android(?:Maven)?Library\b.*?(?:\/>|>\s*<\/Android(?:Maven)?Library>)/m) do |element|
    rewrite_android_library(element, version)
  end

  File.write(file, updated)
end

Dir.chdir(REPO_ROOT)

puts "▸ Applying Android libraries from Maven local".green
puts "▸ Using #{MAVEN_LOCAL}".blue

CSProject.all.select { |project| project.platform == :android }.each do |project|
  puts "▸ Updating #{project.directory}".blue
  update_project(project, options.version)
end

puts "▸ Done. 🚀".green
