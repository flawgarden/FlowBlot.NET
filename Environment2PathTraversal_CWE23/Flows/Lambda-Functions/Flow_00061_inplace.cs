using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using FlowBlot.Model;

namespace FlowBlot
{
    public class Flow_00061_inplace
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
            string input = Flow_00061_inplace.Source();

            MProxy proxy = new MProxy();
            MProxy.Fetch fetchDelegate = new MProxy.Fetch(proxy.FetchNoPropagate);
            string output = fetchDelegate(input);


            /*FLOW:Flow_00061_inplace - A delegate FP taint propagation:codethreat.flowblot.benchmark:6:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00061_inplace.Sink(output);
        }
    }
}
