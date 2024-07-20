using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00067_inplace
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
            Blot blot1 = new Blot();
            Blot blot2 = new Blot();

            blot1.Name = Flow_00067_inplace.Source();

            /*FLOW:Flow_00067_inplace - A points-to analysis FP taint propagation:codethreat.flowblot.benchmark:0:NONE:0:
             *STEP_PATH:ABC
             */
            Flow_00067_inplace.Sink(blot2.Name);
        }
    }
}
