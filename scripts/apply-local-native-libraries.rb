require 'optparse'
require 'ostruct'
require 'shellwords'
require_relative 'lib/project'
require_relative 'lib/system'
require_relative 'lib/terminal'

options = OpenStruct.new

OptionParser.new do |opt|
  opt.on('-p', '--platform PLATFORM', String, [:android, :ios], 'The platform to update (android, ios).') { |o| options.platform = o }
  opt.on('-v', '--version VERSION', String, 'The native library version.') { |o| options.version = o }
end.parse!

def options.require_argument(key, message)
  raise OptionParser::MissingArgument, message if self[key].nil?
end

options.require_argument('version', 'The version is required. Please provide it by using -v or --version.')

def command(*parts)
  Shellwords.join(parts.compact)
end

def platform_argument(platform)
  return [] if platform.nil?

  ['--platform', platform.to_s]
end

def should_update_platform?(selected_platform, platform)
  selected_platform.nil? || selected_platform == platform
end

repo_root = File.expand_path('..', __dir__)
scripts_dir = File.join(repo_root, 'scripts')

Dir.chdir(repo_root)

if should_update_platform?(options.platform, :ios)
  execute_system_command(
    command(
      'ruby',
      File.join(scripts_dir, 'local', 'copy-xcframeworks.rb'),
      '--version',
      options.version
    )
  )
  puts
end

if should_update_platform?(options.platform, :android)
  execute_system_command(
    command(
      'ruby',
      File.join(scripts_dir, 'local', 'apply-android-libraries.rb'),
      '--version',
      options.version
    )
  )
  puts
end

puts "▸ Done. 🚀".green
