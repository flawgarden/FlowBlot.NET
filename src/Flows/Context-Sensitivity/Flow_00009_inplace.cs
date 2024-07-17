using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00009_inplace
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
            string input = Flow_00009_inplace.Source();
            BlotBag blotBag = new BlotBag();
            Blot newBlot = new Blot();
            newBlot.Name = input;
            blotBag.Put(newBlot);

            /*FLOW:Flow_00009_inplace - A context-sensitivity taint propagation:codethreat.flowblot.benchmark:8:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00009_inplace.Sink(blotBag.GetFirst().Name);
        }
    }
}
