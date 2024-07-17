using System.Collections.Generic;

namespace FlowBlot
{
    public class Flow_00063_inplace
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
            string input = Flow_00063_inplace.Source();

            List<string> aList = new List<string>()
            {
                input,
                System.String.Empty,
                System.String.Empty
            };

            /*FLOW:Flow_00063_inplace - An inline List definition taint propagation:codethreat.flowblot.benchmark:6:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00063_inplace.Sink(aList[0]);
        }
    }
}
