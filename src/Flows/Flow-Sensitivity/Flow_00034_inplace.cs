namespace FlowBlot.Flows_inplace
{
    public class Flow_00034_inplace
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
            string input = Flow_00034_inplace.Source();

            input = System.String.Empty;

            /*FLOW:Flow_00034_inplace - A flow-sensitive FP taint propagation:codethreat.flowblot.benchmark:4:FIND_ISSUE:1:
            *STEP_PATH:ABC
            */
            Flow_00034_inplace.Sink(input);
        }
    }
}
