using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00047_inplace
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
				try
				{
					/* read string from file into data */
					using (StreamReader sr = new StreamReader("data.txt"))
					{
						/* POTENTIAL FLAW: Read data from a file */
						/* This will be reading the first "line" of the file, which
						 * could be very long if there are little or no newlines in the file */
						data = sr.ReadLine();
					}
				}
				catch (IOException exceptIO)
				{
					System.Console.WriteLine("error within file stream read");
				}
			}
			return data;
		}

        public void Run()
        {
            FirstDrill();
            SecondDrill();
        }

        private void FirstDrill()
        {
            C a = new C();
            C b = a;
            string input = Flow_00047_inplace.Source();
            Mix(a, b, input);
        }

        private void SecondDrill()
        {
            C a = new C();
            C b = new C();
            string input = "My";
            Mix(a, b, input);
        }

        private void Mix(C a, C b, string n)
        {
            a.x = n;

            /*FLOW:Flow_00047_inplace - A complex points-to analysis taint propagation:codethreat.flowblot.benchmark:0+:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00047_inplace.Sink(b.x);
        }

    }
}
