using FlowBlot.Model;

namespace FlowBlot
{
    public class Flow_00065_inplace
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
            string input = Flow_00065_inplace.Source();

            var c = new A();

            var d = c;

            var e = d;

            d.b = input;

            var f = e;

            /*FLOW:Flow_00065_inplace - A complex points-to analysis taint propagation:codethreat.flowblot.benchmark:9:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00065_inplace.Sink(f.b);
        }
    }
}
