﻿using FlowBlot.Model;

namespace FlowBlot
{
    public class Flow_00003_inplace
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
            string input = Flow_00003_inplace.Source();
            Blot aBlot = new Blot();
            aBlot.Name = input;

            /*FLOW:Flow_00003_inplace - A field-sensitive taint propagation:codethreat.flowblot.benchmark:5:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00003_inplace.Sink(aBlot.Name);
        }
    }
}
