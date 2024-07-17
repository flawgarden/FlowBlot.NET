using FlowBlot.Model;

namespace FlowBlot
{
    public class Flow_00004_inplace
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
            string input = Flow_00004_inplace.Source();
            Blot aBlot = new Blot();
            aBlot.Description = input;

            /*FLOW:Flow_00004_inplace - A field-sensitive FP taint propagation:codethreat.flowblot.benchmark:5:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00004_inplace.Sink(aBlot.Name);
        }
    }
}
