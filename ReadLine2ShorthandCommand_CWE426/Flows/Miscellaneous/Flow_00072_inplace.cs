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
    public class Flow_00072_inplace
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

        // a step towards soundness. not precise. trust level matters: medium|low

        public void Run()
        {
            string input = Flow_00072_inplace.Source();
            IWeatherService weatherService = WeatherServiceFinder.FetchProvider();
            string passThrough = weatherService.GetWeatherData(input);

            /*FLOW:Flow_00072_inplace - A 3rd-party code taint propagation:codethreat.flowblot.benchmark:6:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00072_inplace.Sink(passThrough);
        }
    }
}
