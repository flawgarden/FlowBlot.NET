
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
    public class Flow_00010_inplace
    {
		public static void Sink(string input)
		{
			/* FLAW: Execute command without the full path */
			string badOsCommand;
			string commandArgs;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				badOsCommand = "ls";
				commandArgs = "-la";
			}
			else
			{
				badOsCommand = "cmd.exe";
				commandArgs = "/C dir";
			}
			using (Process process = new Process())
			{
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.FileName = badOsCommand;
				startInfo.Arguments = commandArgs;
				process.StartInfo = startInfo;
				process.StartInfo.UseShellExecute = false;
				process.Start();
			}
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
            string input = Flow_00010_inplace.Source();

            BlotBag blotBag = new BlotBag();
            Blot newBlot = new Blot();
            newBlot.Name = input;
            blotBag.Put(newBlot);

            BlotBag blotBag2 = new BlotBag();
            Blot newBlot2 = new Blot();
            newBlot2.Name = System.String.Empty;
            blotBag2.Put(newBlot2);

            /*FLOW:Flow_00010_inplace - A context-sensitive FP taint propagation:codethreat.flowblot.benchmark:9:NONE:1:
             *STEP_PATH:ABC
             */
            Flow_00010_inplace.Sink(blotBag2.GetFirst().Name);
        }
    }
}
