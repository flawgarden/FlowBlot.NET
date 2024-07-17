using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00068_inplace
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
            var blot1 = new Blot();
            var blot2 = new Blot();

            blot2.Name = Flow_00068_inplace.Source();

            blot2 = blot1;

            /*FLOW:Flow_00068_inplace - A flow-sensitive points-to analysis FP taint propagation:codethreat.flowblot.benchmark:4:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00068_inplace.Sink(blot2.Name);
        }
    }
}
