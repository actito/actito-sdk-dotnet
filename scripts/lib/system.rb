require 'open3'

def execute_system_command(command)
  system(command, exception: true)
end

def capture_system_command_output(command, stream_output = false)
  Open3.popen3(command) do |stdin, stdout, stderr, wait_thread|
    output = '';

    Thread.new do
      stdout.each { |l|
        puts l
        output << l
      }
    end if stream_output

    exit_status = wait_thread.value

    return exit_status, output
  end
end
