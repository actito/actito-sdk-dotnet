require 'fileutils'
require 'json'
require 'open-uri'
require 'zip'
require_relative 'lib/project'
require_relative 'lib/terminal'

raise ArgumentError, "Unexpected arguments: #{ARGV.join(' ')}" unless ARGV.empty?

package_resolved_path = File.expand_path(
  '../Actito.Bindings.xcworkspace/xcshareddata/swiftpm/Package.resolved',
  __dir__
)
package_resolved = JSON.parse(File.read(package_resolved_path))
actito_sdk_pin = package_resolved.fetch('pins').find do |pin|
  pin.fetch('identity') == 'actito-sdk-ios'
end

raise 'Could not find actito-sdk-ios in Package.resolved.' if actito_sdk_pin.nil?

version = actito_sdk_pin.fetch('state').fetch('version')

puts "▸ Creating temporary directory".green
FileUtils.rm_rf('.tmp')
FileUtils.mkdir('.tmp')

puts "▸ Downloading XCFrameworks".green
open("https://cdn-mobile.actito.com/libs/ios/#{version}/cocoapods.zip") do |file|
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
