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
    public class Flow_00024_inplace
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

        public void Run(string input)
        {
            input += Flow_00024_inplace.Source();


            /*FLOW:Flow_00024_inplace - A multi source taint propagation:codethreat.flowblot.benchmark:3:FIND_ISSUE:1:2:
             *STEP_PATH:ABC
             */
            Flow_00024_inplace.Sink(input);
        }
    }
}
