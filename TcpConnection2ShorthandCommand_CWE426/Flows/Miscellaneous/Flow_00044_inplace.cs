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
			/* Read data using an outbound tcp connection */
			{
				try
				{
					/* Read data using an outbound tcp connection */
					using (TcpClient tcpConn = new TcpClient("host.example.org", 39544))
					{
						/* read input from socket */
						using (StreamReader sr = new StreamReader(tcpConn.GetStream()))
						{
							/* POTENTIAL FLAW: Read data using an outbound tcp connection */
							data = sr.ReadLine();
						}
					}
				}
				catch (IOException exceptIO)
				{
					System.Console.WriteLine("error within reading tcp stream");
				}
			}
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
