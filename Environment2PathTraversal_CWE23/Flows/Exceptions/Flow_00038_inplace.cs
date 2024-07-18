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
    public class Flow_00038_inplace
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
            try
            {
                string input = Flow_00038_inplace.Source();

                ThrowThat(input);
            }
            catch(System.Exception e)
            {
              /*FLOW:Flow_00038_inplace - An exception handler taint propagation:codethreat.flowblot.benchmark:0+:FIND_ISSUE:1:
              *STEP_PATH:ABC
              */
                Flow_00038_inplace.Sink(e.Message);
            }
        }

        public void ThrowThat(string i)
        {

            throw new System.Exception(i);
        }

    }
}
