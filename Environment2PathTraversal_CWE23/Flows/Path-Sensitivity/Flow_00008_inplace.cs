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
    public class Flow_00008_inplace
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
			/* get environment variable ADD */
			/* POTENTIAL FLAW: Read data from an environment variable */
			data = Environment.GetEnvironmentVariable("ADD");
			return data;
		}

        public void Run()
        {
            string input = Flow_00008_inplace.Source();            
            if (5 > 1)
            {
                input = System.String.Empty;
            }

            /*FLOW:Flow_00008_inplace - A hard-coded if FP taint propagation:codethreat.flowblot.benchmark:4:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00008_inplace.Sink(input);
        }
    }
}
