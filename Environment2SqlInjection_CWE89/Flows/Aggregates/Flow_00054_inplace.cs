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
    public class Flow_00054_inplace
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
			/* get environment variable ADD */
			/* POTENTIAL FLAW: Read data from an environment variable */
			data = Environment.GetEnvironmentVariable("ADD");
			return data;
		}

       public void Run()
        {
            string input = Flow_00054_inplace.Source();
            var inputs = new Queue<string>();
            inputs.Enqueue(input);
            inputs.Enqueue(string.Empty);
            inputs.Enqueue(string.Empty);
            if (inputs != null && inputs.Count > 0)
            {
                /*FLOW:Flow_00054_inplace - A Queue taint propagation:codethreat.flowblot.benchmark:5:FIND_ISSUE:1:
                 *STEP_PATH:ABC
                 */
                Flow_00054_inplace.Sink(inputs.Dequeue());
            }
        }
    }
}
