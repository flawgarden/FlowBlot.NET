
using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace FlowBlot
{
    public class Flow_00064_inplace
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
            string input = Flow_00064_inplace.Source();

            string [] anArray = new string []
            {
                input,
                System.String.Empty,
                System.String.Empty
            };

            /*FLOW:Flow_00064_inplace - An inline Array definition taint propagation:codethreat.flowblot.benchmark:5:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00064_inplace.Sink(anArray[0]);
        }
    }
}
