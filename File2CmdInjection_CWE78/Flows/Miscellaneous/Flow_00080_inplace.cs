
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
    public class Flow_00080_inplace
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
			data = ""; /* Initialize data */
			{
				try
				{
					/* read string from file into data */
					using (StreamReader sr = new StreamReader("data.txt"))
					{
						/* POTENTIAL FLAW: Read data from a file */
						/* This will be reading the first "line" of the file, which
						 * could be very long if there are little or no newlines in the file */
						data = sr.ReadLine();
					}
				}
				catch (IOException exceptIO)
				{
					System.Console.WriteLine("error within file stream read");
				}
			}
			return data;
		}

        public void Run()
        {
            string input = Flow_00080_inplace.Source();
            RunMe(arg: input, one: string.Empty);
        }

        private void RunMe(string one, string arg)
        {

            /*FLOW:Flow_00080_inplace - A named argument method call:codethreat.flowblot.benchmark:5:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00080_inplace.Sink(arg);
        }
    }
}
