using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using System.Collections.Generic;
using System.Reflection;

namespace FlowBlot
{
    public class Flow_00062_inplace
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

        // This case, in fact, is neither true nor false. 
        // For soundiness, it will be handled as true. 

        public void Run()
        {
            string input = Flow_00062_inplace.Source();

            Assembly flowAssembly = Assembly.LoadFile(@"flow.dll");
            System.Type flowAssemblyType = flowAssembly.GetType("Flow.Fetcher");

            object flowInstance = System.Activator.CreateInstance(flowAssemblyType);

            MethodInfo getMethod = flowAssemblyType.GetMethod("Get");

            List<object> arguments = new List<object>();
            arguments.Add(input);
            arguments.Add(System.String.Empty);

            object output = getMethod.Invoke(flowInstance, arguments.ToArray());

            /*FLOW:Flow_00062_inplace - A reflection taint propagation:codethreat.flowblot.benchmark:7:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00062_inplace.Sink(output.ToString());
        }
    }
}
