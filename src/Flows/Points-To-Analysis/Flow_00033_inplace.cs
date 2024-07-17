using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00033_inplace
    {
		public static void Sink(string input)
		{
			/* SINK GENERATED CODE */
		}

		private static string Source()
		{
			string data = string.Empty;
			/* SOURCE GENERATED CODE */
			return data;
		}

        public void Run()
        {

            string input1 = System.String.Empty;

            DataSource ds1 = new DataSource(input1);

            string input2 = Flow_00033_inplace.Source();

            DataSource ds2 = new DataSource(input2);

            string output1 = ds1.getUrl();

            string output2 = ds2.getUrl();

            /*FLOW:Flow_00033_inplace - A context-sensitive points-to analysis taint propagation:codethreat.flowblot.benchmark:12:FIND_ISSUE:1:
            *STEP_PATH:ABC
            */
            Flow_00033_inplace.Sink(output2);
        }
    }
}
