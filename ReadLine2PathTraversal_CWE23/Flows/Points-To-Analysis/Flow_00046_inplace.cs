using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using System;
using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00046_inplace
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
            string input = Flow_00046_inplace.Source();

            A b, q, y;
            B a, p, x;

            a = new B();
            p = new B();

            b = new A();
            q = new A();

            if ((new Random()).Next(0, 1) < 0.5f)
            {
                x = a;
                y = q;
            }
            else
            {
                x = p;
                y = b;
            }

            x.attr = y;
            q.b = input;

            /*FLOW:Flow_00046_inplace - A complex alias shuffling points-to analysis taint propagation:codethreat.flowblot.benchmark:0+:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00046_inplace.Sink(a.attr.b);
        }

    }
}