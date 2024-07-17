using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00020_inplace
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
            Blot blot = new Blot();
            blot.Viscosity = Flow_00020_inplace.Source();


            /*FLOW:Flow_00020_inplace - A setter FP taint propagation:codethreat.flowblot.benchmark:0:NONE:0:
             *STEP_PATH:ABC
             */
            Flow_00020_inplace.Sink(blot.Viscosity);
        }

    }
}
