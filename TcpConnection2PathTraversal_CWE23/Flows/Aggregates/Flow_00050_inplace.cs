using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using System.Collections;

namespace FlowBlot
{
    public class Flow_00050_inplace
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
            string input = Flow_00050_inplace.Source();
            Hashtable inputs = new Hashtable();
            inputs.Add("key", input);
            inputs.Add("key1",string.Empty);
            inputs.Add("key2", string.Empty);
            if (inputs != null && inputs.Count > 0)
            {
                /*FLOW:Flow_00050_inplace - A HashTable taint propagation:codethreat.flowblot.benchmark:5:FIND_ISSUE:1:
                 *STEP_PATH:ABC
                 */
                Flow_00050_inplace.Sink(inputs["key"].ToString());
            }
        }
    }
}