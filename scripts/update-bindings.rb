require 'optparse'
require 'ostruct'
require_relative 'lib/project'
require_relative 'lib/system'
require_relative 'lib/terminal'

options = OpenStruct.new
OptionParser.new do |opt|
  opt.on('-c', '--component COMPONENT', String, [:assets, :core, :geo, :in_app_messaging, :inbox, :loyalty, :notification_service_extension, :push, :push_ui, :scannables, :user_inbox], 'The component to update (assets, core, geo, in_app_messaging inbox, loyalty, push, push_ui, scannables, user_inbox).') { |o| options.component = o }
  opt.on('-p', '--platform PLATFORM', String, [:android, :ios], 'The platform to update (android, ios).') { |o| options.platform = o }
  opt.on('--clean', 'Clean project before building.') { |o| options.clean = o }
end.parse!

def options.require_argument(key, message)
    raise OptionParser::MissingArgument, message if self[key].nil?
end

options.require_argument('component', 'The component is required. Please provide it by using -c or --component.')
options.require_argument('platform', 'The platform is required. Please provide it by using -p or --platform.')


project = CSProject.find(options.component, options.platform)
raise ArgumentError.new('Unable to find matching project.') if project.nil?

if options.clean
  puts "▸ Cleaning project".green
  project.clean
end

puts "▸ Building project".green
project.build

if project.platform == :ios
  puts "▸ Generating binding source files".green
  project.generate_binding_sources

  puts "▸ Rebuilding project".green
  project.build
end

puts "▸ Done. 🚀".green
