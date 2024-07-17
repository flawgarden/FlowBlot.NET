namespace FlowBlot.Flows_inplace
{
    public class Flow_00008_inplace
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
            string input = Flow_00008_inplace.Source();            
            if (5 > 1)
            {
                input = System.String.Empty;
            }

            /*FLOW:Flow_00008_inplace - A hard-coded if FP taint propagation:codethreat.flowblot.benchmark:4:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00008_inplace.Sink(input);
        }
    }
}
