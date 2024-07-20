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
    public class Flow_00027_inplace
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
            string input = Flow_00027_inplace.Source();
            string condition = Flow_00027_inplace.Source();

            switch (condition)
            {
                case "one":
                    input = System.String.Empty;
                    break;
                case "two":
                case "three":
                    break;
                default:
                    break;
            }

            /*FLOW:Flow_00027_inplace - A switch taint propagation:codethreat.flowblot.benchmark:4:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00027_inplace.Sink(input);
        }
    }
}
