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
    public class Flow_00058_inplace
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
				SqlConnection dbConnection = new SqlConnection(connetionString);
				/* POTENTIAL FLAW: Set the database user name with the value of data
				 * allowing unauthorized access to a portion of the DB */
				dbConnection.ConnectionString = @"Data Source=" + "" + ";Initial Catalog=" + "" + ";User ID=" + input + ";Password=" + "";
				dbConnection.Open();
			}
			catch (SqlException exceptSql)
			{
				System.Console.WriteLine("error within execution of sql command");
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
            string input = Flow_00058_inplace.Source();

            A a = new A();
            a.b = input;

            G g = new H();
            A b = g.Id(a);

            /*FLOW:Flow_00058_inplace - An interface based alias analysis taint propagation:codethreat.flowblot.benchmark:8:FIND_ISSUE:1:
              *STEP_PATH:ABC
              */
            Flow_00058_inplace.Sink(b.b);
        }
    }
}
