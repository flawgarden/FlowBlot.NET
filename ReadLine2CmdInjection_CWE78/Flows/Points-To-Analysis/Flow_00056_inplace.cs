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
    public class Flow_00056_inplace
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
            string input = Flow_00056_inplace.Source();

            D a = new D();
            a.x = input;
            D b = new D();

            E e = new E(a);
            e.ReInitialize(b);
            D c = e.Get();

            /*FLOW:Flow_00056_inplace - A basic alias analysis FP taint propagation:codethreat.flowblot.benchmark:0:NONE:0:
              *STEP_PATH:ABC
              */
            Flow_00056_inplace.Sink(c.x);
        }
    }
}
