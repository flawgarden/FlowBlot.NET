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
    public class Flow_00059_inplace
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

        public void Run()
        {
            string input = Flow_00059_inplace.Source();

            A a = new A();
            a.b = input;
            A b = new A();

            /*FLOW:Flow_00059_inplace - A flow-sensitive FP taint propagation:codethreat.flowblot.benchmark:0:NONE:0:
             *STEP_PATH:ABC
             */
            Flow_00059_inplace.Sink(b.b);
            b = a;
        }
    }
}
