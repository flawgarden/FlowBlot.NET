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
    public class Flow_00079_inplace
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
            string input = Flow_00079_inplace.Source();


            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.RedirectStandardInput = true;
                process.Start();
                using (System.IO.StreamWriter writer = process.StandardInput)
                {

                    /*FLOW:Flow_00079_inplace - A wrapper based intriguing hybrid taint flow:codethreat.flowblot.benchmark:0+:FIND_ISSUE:1:
                     *STEP_PATH:ABC
                     */
                    writer.WriteLine(input);
                }
            }
        }
    }
}
