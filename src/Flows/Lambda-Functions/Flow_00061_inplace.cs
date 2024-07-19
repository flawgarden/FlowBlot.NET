﻿using FlowBlot.Model;

namespace FlowBlot
{
    public class Flow_00061_inplace
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
            string input = Flow_00061_inplace.Source();

            MProxy proxy = new MProxy();
            MProxy.Fetch fetchDelegate = new MProxy.Fetch(proxy.FetchNoPropagate);
            string output = fetchDelegate(input);


            /*FLOW:Flow_00061_inplace - A delegate FP taint propagation:codethreat.flowblot.benchmark:6:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00061_inplace.Sink(output);
        }
    }
}