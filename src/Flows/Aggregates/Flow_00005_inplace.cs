using System.Collections.Generic;

namespace FlowBlot
{
    public class Flow_00005_inplace
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
            string input = Flow_00005_inplace.Source();
            List<string> inputs = new List<string>();
            inputs.Add(input);
            inputs.Add(string.Empty);
            inputs.Add(string.Empty);
            if (inputs != null && inputs.Count > 0)
            {

                /*FLOW:Flow_00005_inplace - A List item taint propagation:codethreat.flowblot.benchmark:4:FIND_ISSUE:1:
                 *STEP_PATH:ABC
                 */
                Flow_00005_inplace.Sink(inputs[0]);
            }
        }
    }
}
