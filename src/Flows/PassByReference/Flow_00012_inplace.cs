using FlowBlot.Model;
using System;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00012_inplace
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
            Blot blot = new Blot() { Name = String.Empty };
            Color(blot);

            /*FLOW:Flow_00012_inplace - A pass by reference taint propagation:codethreat.flowblot.benchmark:5:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00012_inplace.Sink(blot.Name);
        }

        public void Color(Blot blot)
        {
            blot.Name = Flow_00012_inplace.Source();
        }
    }
}
