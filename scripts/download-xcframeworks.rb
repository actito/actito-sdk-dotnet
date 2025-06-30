require 'fileutils'
require 'open-uri'
require 'optparse'
require 'ostruct'
require 'zip'
require_relative 'lib/project'
require_relative 'lib/terminal'

options = OpenStruct.new
OptionParser.new do |opt|
  opt.on('-v', '--version VERSION', String, 'The version to download.') { |o| options.version = o }
end.parse!

def options.require_argument(key, message)
  raise OptionParser::MissingArgument, message if self[key].nil?
end

options.require_argument('version', 'The version is required. Please provide it by using -v or --version.')


puts "▸ Creating temporary directory".green
FileUtils.rm_rf('.tmp')
FileUtils.mkdir('.tmp')

puts "▸ Downloading XCFrameworks".green
open("https://cdn.notifica.re/libs/ios/#{options.version}/cocoapods.zip") do |file|
  Zip::File.open_buffer(file.read) do |zip_file|
    zip_file.each do |f|
      fpath = File.join('.tmp', f.name)
      zip_file.extract(f, fpath)
    end
  end
end

puts "▸ Updating binding XCFrameworks".green
ios_projects = CSProject.all.select { |e| e.platform == :ios }
ios_projects.each do |project|
  puts "▸ Updating #{project.directory}".blue

  destination = File.join(project.directory, 'libs')

  FileUtils.mkdir(destination) unless File.exists?(destination)
  FileUtils.rm_rf(File.join(destination, '*.xcframework'))

  project.xcframeworks.each do |xcframework|
    puts "▸ Copying #{xcframework}".blue

    FileUtils.cp_r(
      File.join('.tmp', 'Actito', xcframework),
      destination
    )
  end
end

FileUtils.rm_rf('.tmp')

puts "▸ Done. 🚀".green
