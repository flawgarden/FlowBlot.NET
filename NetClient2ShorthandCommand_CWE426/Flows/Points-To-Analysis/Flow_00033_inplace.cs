using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00033_inplace
    {
		public static void Sink(string input)
		{
			/* FLAW: Execute command without the full path */
			string badOsCommand;
			string commandArgs;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				badOsCommand = "ls";
				commandArgs = "-la";
			}
			else
			{
				badOsCommand = "cmd.exe";
				commandArgs = "/C dir";
			}
			using (Process process = new Process())
			{
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.FileName = badOsCommand;
				startInfo.Arguments = commandArgs;
				process.StartInfo = startInfo;
				process.StartInfo.UseShellExecute = false;
				process.Start();
			}
		}

		private static string Source()
		{
			string data = string.Empty;
			data = ""; /* Initialize data */
			/* read input from WebClient */
			{
				try
				{
					using (WebClient client = new WebClient())
					{
						using (StreamReader sr = new StreamReader(client.OpenRead("http://www.example.org/")))
						{
							/* POTENTIAL FLAW: Read data from a web server with WebClient */
							/* This will be reading the first "line" of the response body,
							* which could be very long if there are no newlines in the HTML */
							data = sr.ReadLine();
						}
					}
				}
				catch (IOException exceptIO)
				{
					System.Console.WriteLine("error within stream reading");
				}
			}
			return data;
		}

        public void Run()
        {

            string input1 = System.String.Empty;

            DataSource ds1 = new DataSource(input1);

            string input2 = Flow_00033_inplace.Source();

            DataSource ds2 = new DataSource(input2);

            string output1 = ds1.getUrl();

            string output2 = ds2.getUrl();

            /*FLOW:Flow_00033_inplace - A context-sensitive points-to analysis taint propagation:codethreat.flowblot.benchmark:12:FIND_ISSUE:1:
            *STEP_PATH:ABC
            */
            Flow_00033_inplace.Sink(output2);
        }
    }
}
