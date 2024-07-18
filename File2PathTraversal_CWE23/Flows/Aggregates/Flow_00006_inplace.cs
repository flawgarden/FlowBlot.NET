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
    public class Flow_00006_inplace
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
			{
				try
				{
					/* read string from file into data */
					using (StreamReader sr = new StreamReader("data.txt"))
					{
						/* POTENTIAL FLAW: Read data from a file */
						/* This will be reading the first "line" of the file, which
						 * could be very long if there are little or no newlines in the file */
						data = sr.ReadLine();
					}
				}
				catch (IOException exceptIO)
				{
					System.Console.WriteLine("error within file stream read");
				}
			}
			return data;
		}

        public void Run()
        {
            string input = Flow_00006_inplace.Source();
            List<string> inputs = new List<string>();
            inputs.Add(string.Empty);
            inputs.Add(input);
            inputs.Add(string.Empty);
            if (inputs != null && inputs.Count > 0)
            {
                /*FLOW:Flow_00006_inplace - A List item FP taint propagation:codethreat.flowblot.benchmark:4:FIND_ISSUE:1:
                 *STEP_PATH:ABC
                 */
                Flow_00006_inplace.Sink(inputs[0]);
            }
        }
    }
}
