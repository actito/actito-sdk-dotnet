require 'optparse'
require 'ostruct'
require_relative 'lib/project'
require_relative 'lib/system'
require_relative 'lib/terminal'

options = OpenStruct.new
OptionParser.new do |opt|
  opt.on('-p', '--platform PLATFORM', String, [:android, :ios], 'The platform to update (android, ios).') { |o| options.platform = o }
  opt.on('--clean', 'Clean project before building.') { |o| options.clean = o }
end.parse!

projects = CSProject.all.filter { |e| options.platform.nil? || e.platform == options.platform }

if options.clean
  projects.each do |project|
    puts "▸ Cleaning project: #{project.name}".green
    project.clean
  end
end

projects.each do |project|
  puts "▸ Building project: #{project.name}".green
  project.build

  if project.platform == :ios
    puts "▸ Generating binding source files".green
    project.generate_binding_sources

    puts "▸ Rebuilding project".green
    project.build
  end
end

puts "▸ Done. 🚀".green
