using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00069_inplace
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

            blot1.Name = Flow_00069_inplace.Source();


            /*FLOW:Flow_00069_inplace - A field-sensitive points-to analysis FP taint propagation:codethreat.flowblot.benchmark:3:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00069_inplace.Sink(blot1.Description);
        }
    }
}
