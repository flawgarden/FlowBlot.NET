
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
    public class Flow_00073_inplace
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

        // a step towards soundness. not precise. trust level matters: medium|low

        public void Run()
        {
            string input = Flow_00073_inplace.Source();

            IShoppingCart cart = ShoppingCartFactory.FetchCart();

            IItem milk = ItemFactory.FetchMilk();
            milk.SetName(input);

            cart.PutItem(milk);

            IItem anItem = cart.FetchItem(0);

            /*FLOW:Flow_00073_inplace - A more complex 3rd-party code taint propagation:codethreat.flowblot.benchmark:0+:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00073_inplace.Sink(anItem.GetName());
        }
    }
}
