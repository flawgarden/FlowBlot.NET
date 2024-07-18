using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using System;
using System.Text;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00035_inplace
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
            string input = Flow_00035_inplace.Source();
            input = Method1(input);


            /*FLOW:Flow_00035_inplace - A deep recursive taint propagation:codethreat.flowblot.benchmark:6:FIND_ISSUE:1:
            *STEP_PATH:ABC
            */
            Flow_00035_inplace.Sink(input);
        }

        public string Method1(string input)
        {
            string output = Method2(String.Empty, String.Empty, input);

            return output;
        }

        public string Method2(string input, string input2, string input3)
        {
            bool result = Method3(input.Length, 5);
            if(result)
            {
                return input3;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(input);
            sb.AppendLine(input2);
            sb.AppendLine(input3);

            return Method2(sb.ToString(), String.Empty, "default");
        }

        public bool Method3(int x, int y)
        {
            if(x > y)
            {
                // do smt.
                if(5 > Method4(y))
                {
                    return true;
                }
            }
            else
            {
                // do smt. else
            }

            return false;
        }

        public int Method4(int y)
        {
            if (y == 6)
                return y++;

            return y--;
        }
    }
}
