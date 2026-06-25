require 'fileutils'
require 'optparse'
require 'ostruct'
require 'open3'
require_relative '../lib/project'
require_relative '../lib/terminal'

options = OpenStruct.new

OptionParser.new do |opt|
  opt.on('-v', '--version VERSION', String, 'The expected XCFrameworks version.') do |o|
    options.version = o
  end
end.parse!

def options.require_argument(key, message)
  raise OptionParser::MissingArgument, message if self[key].nil?
end

options.require_argument('version', 'The version is required. Please provide it by using -v or --version.')

REPO_ROOT = File.expand_path('../..', __dir__)

def actito_tmp_path(root)
  File.join(root, 'actito-sdk-ios', '.build', 'tmp')
end

def find_actito_sdk_tmp
  repo_parent = File.expand_path('..', REPO_ROOT)
  repo_grandparent = File.expand_path('..', repo_parent)

  checked_paths = []

  [repo_parent, repo_grandparent].each do |root|
    candidate = actito_tmp_path(root)
    checked_paths << candidate
    return candidate if Dir.exist?(candidate)
  end

  Dir.children(repo_grandparent).sort.each do |child|
    next if child.start_with?('.')

    child_path = File.join(repo_grandparent, child)
    next unless File.directory?(child_path)

    candidate = actito_tmp_path(child_path)
    checked_paths << candidate
    return candidate if Dir.exist?(candidate)
  end

  raise <<~ERROR
    Could not find actito-sdk-ios/.build/tmp.

    Checked:
    #{checked_paths.map { |path| "    - #{path}" }.join("\n")}
  ERROR
end

def plist_string_value(plist_path, key)
  stdout, stderr, status = Open3.capture3(
    '/usr/libexec/PlistBuddy',
    '-c',
    "Print :#{key}",
    plist_path
  )

  return stdout.strip if status.success? && !stdout.strip.empty?

  raise <<~ERROR
    Could not read #{key} from plist.

    Plist:
        #{plist_path}

    Error:
        #{stderr.strip}
  ERROR
end

def actitokit_info_plist(source)
  File.join(
    source,
    'ActitoKit.xcframework',
    'ios-arm64',
    'ActitoKit.framework',
    'Info.plist'
  )
end

Dir.chdir(REPO_ROOT)

puts "▸ Locating local XCFrameworks".green

tmp_path = find_actito_sdk_tmp
source = Dir.exist?(File.join(tmp_path, 'Actito')) ? File.join(tmp_path, 'Actito') : tmp_path

puts "▸ Using #{source}".blue

plist_path = actitokit_info_plist(source)

unless File.file?(plist_path)
  raise "Could not find ActitoKit Info.plist: #{plist_path}"
end

actual_version = plist_string_value(plist_path, 'CFBundleShortVersionString')

if actual_version != options.version
  raise <<~ERROR
    Local XCFrameworks version mismatch.

    Expected:
        #{options.version}

    Found:
        #{actual_version}

    Info.plist:
        #{plist_path}
  ERROR
end

puts "▸ Confirmed version #{actual_version}".green
puts "▸ Updating binding XCFrameworks".green

ios_projects = CSProject.all.select { |e| e.platform == :ios }

ios_projects.each do |project|
  puts "▸ Updating #{project.directory}".blue

  destination = File.join(project.directory, 'libs')
  FileUtils.mkdir_p(destination)

  Dir.glob(File.join(destination, '*.xcframework')).each do |xcframework|
    FileUtils.rm_rf(xcframework)
  end

  project.xcframeworks.each do |xcframework|
    source_xcframework = File.join(source, xcframework)

    unless Dir.exist?(source_xcframework)
      raise "Missing XCFramework: #{source_xcframework}"
    end

    puts "▸ Copying #{xcframework}".blue

    FileUtils.cp_r(source_xcframework, destination)
  end
end

puts "▸ Done. 🚀".green
