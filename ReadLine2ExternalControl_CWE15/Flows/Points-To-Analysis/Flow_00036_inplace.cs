
using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00036_inplace
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

        // flow insensitive points-to analysis lead to FP
        // in other words; non-deterministic choice of the if branch and may-alias points-to analysis leads to FP

        public void Run()
        {
            string input = Flow_00036_inplace.Source();

            A b, q, y;
            B a, p, x;

            a = new B();
            p = new B();

            b = new A();
            q = new A();

            if ((new System.Random()).Next(0,1) < 0.5)
            {
                x = a;
                y = b;
            }
            else
            {
                x = p;
                y = q;
            }

            x.attr = y;
            q.b = input;

            /*FLOW:Flow_00036_inplace - A shuffling points-to analysis FP taint propagation:codethreat.flowblot.benchmark:0:NONE:0:0
            *STEP_PATH:ABC
            */
            Flow_00036_inplace.Sink(a.attr.b);
        }

    }
}
