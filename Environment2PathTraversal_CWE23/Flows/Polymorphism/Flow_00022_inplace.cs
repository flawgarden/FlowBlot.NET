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
    public class Flow_00022_inplace
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
            Atlas atlas = new Atlas();
            atlas.name = Flow_00022_inplace.Source();

            Machine machine = Build(atlas);

            string input = ((Atlas)machine).SayName();



            /*FLOW:Flow_00022_inplace - A inter-procedural polymorphic taint propagation:codethreat.flowblot.benchmark:8:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00022_inplace.Sink(input);
        }

        public Machine Build(Machine machine)
        {
            return machine;
        }

    }
}
