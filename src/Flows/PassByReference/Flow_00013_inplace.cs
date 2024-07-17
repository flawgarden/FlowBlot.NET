using FlowBlot.Model;
using System;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00013_inplace
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
            string input = Flow_00013_inplace.Source();
            Blot blot = new Blot() { Name = String.Empty };
            Color(blot, input);

            /*FLOW:Flow_00013_inplace - A complex pass by reference taint propagation:codethreat.flowblot.benchmark:10:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00013_inplace.Sink(blot.Name);
        }

        public void Color(Blot blot, string input)
        {
            blot.Name = input;
        }
    }
}
