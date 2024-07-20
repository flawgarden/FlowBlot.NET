namespace FlowBlot.Flows_inplace
{
    public class Flow_00007_inplace
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
            string input = System.String.Empty;
            if (5 > 1)
            {
                input = Flow_00007_inplace.Source();
            }
            /*FLOW:Flow_00007_inplace - A hard-code if taint propagation:codethreat.flowblot.benchmark:3:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00007_inplace.Sink(input);
        }
    }
}
