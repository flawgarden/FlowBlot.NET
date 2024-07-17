using System.Collections.Generic;

namespace FlowBlot
{
    public class Flow_00054_inplace
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
            string input = Flow_00054_inplace.Source();
            var inputs = new Queue<string>();
            inputs.Enqueue(input);
            inputs.Enqueue(string.Empty);
            inputs.Enqueue(string.Empty);
            if (inputs != null && inputs.Count > 0)
            {
                /*FLOW:Flow_00054_inplace - A Queue taint propagation:codethreat.flowblot.benchmark:5:FIND_ISSUE:1:
                 *STEP_PATH:ABC
                 */
                Flow_00054_inplace.Sink(inputs.Dequeue());
            }
        }
    }
}
