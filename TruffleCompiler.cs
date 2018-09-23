using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Solidoc
{
    public static class TruffleCompiler
    {
        private static string Run(string workingDirectory, string filename, string arguments)
        {
            Console.WriteLine(arguments);

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = filename,
                    WorkingDirectory = workingDirectory,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }

        public static void Compile(string workingDirectory)
        {
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if(isWindows)
            {
                Run(workingDirectory, "cmd.exe", "c \"truffle compile\"");
                return;
            }

            Run(workingDirectory, "/bin/bash", "-c \"truffle compile\"");
        }
    }
}