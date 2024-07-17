
using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;


namespace FlowBlot.Flows_inplace
{
    public class Flow_00017_inplace
    {
		public static void Sink(string input)
		{
			String osCommand;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				/* running on Windows */
				osCommand = @"c:\WINDOWS\SYSTEM32\cmd.exe /c dir ";
			}
			else
			{
				/* running on non-Windows */
				osCommand = "/bin/ls ";
			}
			/* POTENTIAL FLAW: command injection */
			Process process = Process.Start(osCommand + input);
			process.WaitForExit();
		}

		private static string Source()
		{
			string data = string.Empty;
			/* get environment variable ADD */
			/* POTENTIAL FLAW: Read data from an environment variable */
			data = Environment.GetEnvironmentVariable("ADD");
			return data;
		}

        public void Run()
        {
            string input = Sprinkle(System.Console.ReadLine());

            /*FLOW:Flow_00017_inplace - A recursive taint propagation:codethreat.flowblot.benchmark:3:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00017_inplace.Sink(input);
        }

        public string Sprinkle(string input)
        {
            string output = input + "s";

            if (output.Length < 1000)
            {
                output = Sprinkle(output);
            }

            return output;
        }

    }
}
