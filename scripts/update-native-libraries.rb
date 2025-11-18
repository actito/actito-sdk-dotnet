require 'optparse'
require 'ostruct'
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


projects = CSProject.all.filter { |p| options.platform.nil? || p.platform == options.platform }
schemes = []

projects.each do |project|
  puts "▸ Updating project: #{project.name}".green
  project.update_native_library_version(options.version)

  if project.platform == :ios
    schemes << project.binding_scheme
  end
end

unless schemes.empty?
  puts "▸ Resolving package dependencies".green
  schemes.each do |scheme|
    execute_system_command("xcodebuild -workspace Actito.Bindings.xcworkspace -scheme #{scheme} -resolvePackageDependencies")
  end
end

puts "▸ Done. 🚀".green
