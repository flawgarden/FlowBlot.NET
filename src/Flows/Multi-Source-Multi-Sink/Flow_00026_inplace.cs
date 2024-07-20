
namespace FlowBlot.Flows_inplace
{
    public class Flow_00026_inplace
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

        public void Run(string input)
        {

            /*FLOW:Flow_00026_inplace - A multi sink taint propagation:codethreat.flowblot.benchmark:7:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00026_inplace.Sink(input);
        }
    }
}
