
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
    public class Flow_00036_inplace
    {
		public static void Sink(string input)
		{
			String osCommand;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				/* running on Windows */
				osCommand = @"c:\WINDOWS\SYSTEM32\cmd.exe /c dir ";
			}
			else
			{
				/* running on non-Windows */
				osCommand = "/bin/ls ";
			}
			/* POTENTIAL FLAW: command injection */
			Process process = Process.Start(osCommand + input);
			process.WaitForExit();
		}

		private static string Source()
		{
			string data = string.Empty;
			data = ""; /* Initialize data */
			/* read input from WebClient */
			{
				try
				{
					using (WebClient client = new WebClient())
					{
						using (StreamReader sr = new StreamReader(client.OpenRead("http://www.example.org/")))
						{
							/* POTENTIAL FLAW: Read data from a web server with WebClient */
							/* This will be reading the first "line" of the response body,
							* which could be very long if there are no newlines in the HTML */
							data = sr.ReadLine();
						}
					}
				}
				catch (IOException exceptIO)
				{
					System.Console.WriteLine("error within stream reading");
				}
			}
			return data;
		}

        // flow insensitive points-to analysis lead to FP
        // in other words; non-deterministic choice of the if branch and may-alias points-to analysis leads to FP

        public void Run()
        {
            string input = Flow_00036_inplace.Source();

            A b, q, y;
            B a, p, x;

            a = new B();
            p = new B();

            b = new A();
            q = new A();

            if ((new System.Random()).Next(0,1) < 0.5)
            {
                x = a;
                y = b;
            }
            else
            {
                x = p;
                y = q;
            }

            x.attr = y;
            q.b = input;

            /*FLOW:Flow_00036_inplace - A shuffling points-to analysis FP taint propagation:codethreat.flowblot.benchmark:0:NONE:0:0
            *STEP_PATH:ABC
            */
            Flow_00036_inplace.Sink(a.attr.b);
        }

    }
}
