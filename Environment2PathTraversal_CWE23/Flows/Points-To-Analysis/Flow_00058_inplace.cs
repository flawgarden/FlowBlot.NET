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
    public class Flow_00058_inplace
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
            string input = Flow_00058_inplace.Source();

            A a = new A();
            a.b = input;

            G g = new H();
            A b = g.Id(a);

            /*FLOW:Flow_00058_inplace - An interface based alias analysis taint propagation:codethreat.flowblot.benchmark:8:FIND_ISSUE:1:
              *STEP_PATH:ABC
              */
            Flow_00058_inplace.Sink(b.b);
        }
    }
}
