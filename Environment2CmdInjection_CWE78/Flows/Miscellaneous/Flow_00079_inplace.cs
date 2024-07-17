
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
    public class Flow_00079_inplace
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
            string input = Flow_00079_inplace.Source();


            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.RedirectStandardInput = true;
                process.Start();
                using (System.IO.StreamWriter writer = process.StandardInput)
                {

                    /*FLOW:Flow_00079_inplace - A wrapper based intriguing hybrid taint flow:codethreat.flowblot.benchmark:0+:FIND_ISSUE:1:
                     *STEP_PATH:ABC
                     */
                    writer.WriteLine(input);
                }
            }
        }
    }
}
