using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using System.Collections.Generic;

namespace FlowBlot
{
    public class Flow_00053_inplace
    {
		public static void Sink(string input)
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
					using (SqlCommand badSqlCommand = new SqlCommand(null, dbConnection))
					{
						/* POTENTIAL FLAW: data concatenated into SQL statement used in ExecuteNonQuery(), which could result in SQL Injection */
						badSqlCommand.CommandText = "insert into users (status) values ('updated') where name='" +input+"'";
						badSqlCommand.ExecuteNonQuery();
					}
				}
			}
			catch (SqlException exceptSql)
			{
				System.Console.WriteLine("error within sql command execution");
			}
		}

		private static string Source()
		{
			string data = string.Empty;
			data = ""; /* Initialize data */
			/* Read data using an outbound tcp connection */
			{
				try
				{
					/* Read data using an outbound tcp connection */
					using (TcpClient tcpConn = new TcpClient("host.example.org", 39544))
					{
						/* read input from socket */
						using (StreamReader sr = new StreamReader(tcpConn.GetStream()))
						{
							/* POTENTIAL FLAW: Read data using an outbound tcp connection */
							data = sr.ReadLine();
						}
					}
				}
				catch (IOException exceptIO)
				{
					System.Console.WriteLine("error within reading tcp stream");
				}
			}
			return data;
		}

        public void Run()
        {
            string input = Flow_00053_inplace.Source();
            var inputs = new SortedDictionary<string, string>();
            inputs.Add("key", input);
            inputs.Add("key", string.Empty);
            inputs.Add("key", string.Empty);
            if (inputs != null && inputs.Count > 0)
            {
                /*FLOW:Flow_00053_inplace - A SortedDictionary taint propagation:codethreat.flowblot.benchmark:4:FIND_ISSUE:1:
                 *STEP_PATH:ABC
                 */
                Flow_00053_inplace.Sink(inputs["key"]);
            }
        }
    }
}
