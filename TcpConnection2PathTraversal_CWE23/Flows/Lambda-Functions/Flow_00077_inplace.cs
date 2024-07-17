
using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using FlowBlot.Model;
using System;
using System.Security.Cryptography.X509Certificates;

namespace FlowBlot
{
    public class Flow_00077_inplace
    {
		public static void Sink(string input)
		{
			string root = @"current\directory\";
			/* POTENTIAL FLAW: no validation of concatenated value */
			if (File.Exists(root + input))
				System.Console.WriteLine("Success");
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
            string input = Flow_00077_inplace.Source();

            Func<string, string> myFunc = x => x == "Formal" ? "Welcome" : "Howdy";

            string output = myFunc(input);

            /*FLOW:Flow_00077_inplace - An expression lambda FP taint propagation:codethreat.flowblot.benchmark:6:FIND_ISSUE:1:
            *STEP_PATH:ABC
            */
            Flow_00077_inplace.Sink(output);
        }
    }
}
