
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
    public class Flow_00055_inplace
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
			/* get environment variable ADD */
			/* POTENTIAL FLAW: Read data from an environment variable */
			data = Environment.GetEnvironmentVariable("ADD");
			return data;
		}

        public void Run()
        {
            string input = Flow_00055_inplace.Source();

            D a = new D();
            D b = new D();
            b.x = input;

            E e = new E(a);
            e.ReInitialize(b);
            D c = e.Get();

            /*FLOW:Flow_00055_inplace - A basic alias analysis taint propagation:codethreat.flowblot.benchmark:0+:FIND_ISSUE:1:
              *STEP_PATH:ABC
              */
            Flow_00055_inplace.Sink(c.x);
        }
    }
}
