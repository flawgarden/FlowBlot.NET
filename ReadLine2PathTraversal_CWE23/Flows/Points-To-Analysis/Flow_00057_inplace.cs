
using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using FlowBlot.Model;

namespace FlowBlot
{
    public class Flow_00057_inplace
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
				/* read user input from console with ReadLine */
				try
				{
					/* POTENTIAL FLAW: Read data from the console using ReadLine */
					data = Console.ReadLine();
				}
				catch (IOException exceptIO)
				{
					System.Console.WriteLine("error within console read");
				}
			}
			return data;
		}

        public void Run()
        {
            string input = Flow_00057_inplace.Source();

            D a = new D();
            D b = new D();
            b.x = input;

            F f = new F(a);
            f.ReInitialize(b);
            D c = f.Get();

            /*FLOW:Flow_00057_inplace - A inheritance based alias analysis FP taint propagation:codethreat.flowblot.benchmark:13:FIND_ISSUE:1:
              *STEP_PATH:ABC
              */
            Flow_00057_inplace.Sink(c.x);
        }
    }
}
