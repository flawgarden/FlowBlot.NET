using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00047_inplace
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
            FirstDrill();
            SecondDrill();
        }

        private void FirstDrill()
        {
            C a = new C();
            C b = a;
            string input = Flow_00047_inplace.Source();
            Mix(a, b, input);
        }

        private void SecondDrill()
        {
            C a = new C();
            C b = new C();
            string input = "My";
            Mix(a, b, input);
        }

        private void Mix(C a, C b, string n)
        {
            a.x = n;

            /*FLOW:Flow_00047_inplace - A complex points-to analysis taint propagation:codethreat.flowblot.benchmark:0+:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00047_inplace.Sink(b.x);
        }

    }
}
