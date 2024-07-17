
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
    public class Flow_00018_inplace
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
            string input = Flow_00018_inplace.Source();

            string output = Nest(Nest(Nest(Nest(Nest(Nest(Nest(input)))))));

            /*FLOW:Flow_00018_inplace - A deep nested call taint propagation:codethreat.flowblot.benchmark:16:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00018_inplace.Sink(output);
        }

        public string Nest(string input)
        {
            return input;
        }

    }
}
