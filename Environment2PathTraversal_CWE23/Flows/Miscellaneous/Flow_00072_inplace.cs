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
    public class Flow_00072_inplace
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
