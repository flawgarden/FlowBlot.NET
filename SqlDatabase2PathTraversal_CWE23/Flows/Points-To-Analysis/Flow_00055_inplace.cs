using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using FlowBlot.Model;

namespace FlowBlot
{
    public class Flow_00055_inplace
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
            string input = Flow_00055_inplace.Source();

            D a = new D();
            D b = new D();
            b.x = input;

            E e = new E(a);
            e.ReInitialize(b);
            D c = e.Get();

            /*FLOW:Flow_00055_inplace - A basic alias analysis taint propagation:codethreat.flowblot.benchmark:0+:FIND_ISSUE:1:
              *STEP_PATH:ABC
              */
            Flow_00055_inplace.Sink(c.x);
        }
    }
}
