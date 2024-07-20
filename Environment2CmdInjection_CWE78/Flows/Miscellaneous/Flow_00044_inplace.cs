using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00044_inplace
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
            string input = Flow_00044_inplace.Source();
            Method1(input);
        }

        public void Method1(string input)
        {            
            if (5 > 1)
            {
                Method2(input);
            }

            /*FLOW:Flow_00044_inplace - A source to sink complex trace order:codethreat.flowblot.benchmark:12:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00044_inplace.Sink(input);
        }

        public void Method2(string input)
        {
            input = input.Substring(5);
        }
    }
}
