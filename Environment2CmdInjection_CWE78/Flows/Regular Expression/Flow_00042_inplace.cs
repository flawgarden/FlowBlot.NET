using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using System.Text.RegularExpressions;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00042_inplace
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

        // kinda false, looking at the regex itself
        // but still there's a possibility of a vulnerability

        public void Run()
        {
            string input = Flow_00042_inplace.Source();

            Match match = Regex.Match(input, @"([\d+])");

            if(match.Success)
            {

                /*FLOW:Flow_00042_inplace - A regular expression taint propagation:codethreat.flowblot.benchmark:7:FIND_ISSUE:1:
                 *STEP_PATH:ABC
                 */
                Flow_00042_inplace.Sink(match.Groups[1].Value);
            }
        }

    }
}
