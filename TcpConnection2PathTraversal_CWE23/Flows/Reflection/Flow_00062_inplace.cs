
using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using System.Collections.Generic;
using System.Reflection;

namespace FlowBlot
{
    public class Flow_00062_inplace
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

        // This case, in fact, is neither true nor false. 
        // For soundiness, it will be handled as true. 

        public void Run()
        {
            string input = Flow_00062_inplace.Source();

            Assembly flowAssembly = Assembly.LoadFile(@"flow.dll");
            System.Type flowAssemblyType = flowAssembly.GetType("Flow.Fetcher");

            object flowInstance = System.Activator.CreateInstance(flowAssemblyType);

            MethodInfo getMethod = flowAssemblyType.GetMethod("Get");

            List<object> arguments = new List<object>();
            arguments.Add(input);
            arguments.Add(System.String.Empty);

            object output = getMethod.Invoke(flowInstance, arguments.ToArray());

            /*FLOW:Flow_00062_inplace - A reflection taint propagation:codethreat.flowblot.benchmark:7:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00062_inplace.Sink(output.ToString());
        }
    }
}
