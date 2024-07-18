using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00023_inplace
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
			/* Read data from a database */
			{
				string sverName = "";
				string Name = "";
				string Username = "";
				string Password = "";
				string connetionString = @"Data Source=" + sverName + ";Initial Catalog=" + Name + ";User ID=" + Username + ";Password=" + Password;
				try
				{
					using (SqlConnection dbConnection = new SqlConnection(connetionString))
					{
						dbConnection.Open();
						/* prepare and execute a (hardcoded) query */
						using (SqlCommand command = new SqlCommand(null, dbConnection))
						{
							command.CommandText = "select name from users where id=0";
							command.Prepare();
							using (SqlDataReader dr = command.ExecuteReader())
							{
								/* POTENTIAL FLAW: Read data from a database query SqlDataReader */
								data = dr.GetString(1);
							}
						}
					}
				}
				catch (SqlException exceptSql)
				{
					System.Console.WriteLine("error within sql command execution");
				}
			}
			return data;
		}

        public void Run()
        {
            string input = Flow_00023_inplace.Source();

            /*FLOW:Flow_00023_inplace - A multi source taint propagation:codethreat.flowblot.benchmark:0:NONE:0:
             *STEP_PATH:ABC
             */
            new Flow_00024_inplace().Run(input);
        }
    }
}
