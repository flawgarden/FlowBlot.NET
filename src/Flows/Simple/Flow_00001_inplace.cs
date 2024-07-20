namespace FlowBlot.Flows_inplace
{
    public class Flow_00001_inplace
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
            string input = Flow_00001_inplace.Source();

            /*FLOW:Flow_00001_inplace - A Basic source to sink taint propagation:codethreat.flowblot.benchmark:3:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00001_inplace.Sink(input);
        }
    }
}
