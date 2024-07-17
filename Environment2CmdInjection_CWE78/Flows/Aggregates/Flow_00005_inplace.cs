
using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using System.Collections.Generic;

namespace FlowBlot
{
    public class Flow_00005_inplace
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
            string input = Flow_00005_inplace.Source();
            List<string> inputs = new List<string>();
            inputs.Add(input);
            inputs.Add(string.Empty);
            inputs.Add(string.Empty);
            if (inputs != null && inputs.Count > 0)
            {

                /*FLOW:Flow_00005_inplace - A List item taint propagation:codethreat.flowblot.benchmark:4:FIND_ISSUE:1:
                 *STEP_PATH:ABC
                 */
                Flow_00005_inplace.Sink(inputs[0]);
            }
        }
    }
}
