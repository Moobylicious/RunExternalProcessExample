using System;
using System.IO;

namespace IronPythonTest
{
    class Program
    {
        static int Main(string[] args)
        {
            //This is the 32-bit program which can be 'plugged in' to the 32-bit process.  I'm using a console app here,
            //but this code could go in a class library as an 'activity' or whatever.

            //create  process to run an external program...
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //hide it - don't spawn a CMD window
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";

            //add command line params - guess these would be passed in somehow?
            var arg1 = "function_name";
            var arg2 = "input1";
            var arg3 = "input2";


            //Build command line.  Note this is pointing to a '64-bit' process that I wrote (.net, targets x64)
            //
            startInfo.Arguments = $@"/C ..\..\..\Some64BitProcess\bin\Debug\Some64BitProcess.exe {arg1} {arg2} {arg3}";

            //I don't know what the output of this will be.  whether it creates a file or just prints stuff out on 
            //the command line (STDOUT).  
            //it is possible to capture STDOUT into a stream, but for simplicity I'll simply re-direct it into
            //an output file via the command line '>' which will overwrite any file already there.
            startInfo.Arguments += " > output.txt";
            process.StartInfo = startInfo;


            //Run process
            process.Start();

            //wait for it to complete - note, if this process pauses for user input at any point this will basically just hang indefinitely.
            //you CAN pipe a 'Y' into it or something to avoid this(simulate a keypress), but would be better to work out command line
            //switches that supress any waiting for user input.
            process.WaitForExit();


            //now, we can get results.

            var output = File.ReadAllText("output.txt");

            //Do things with output?
            Console.WriteLine(output);
            Console.ReadLine();
            
            //process.ExitCode is the Exit Code of the command.  This might be useful as it might indicate specific errors encountered
            //by Python.
            return process.ExitCode;            
        }
    }
}
