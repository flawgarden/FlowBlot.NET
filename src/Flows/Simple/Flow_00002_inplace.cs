namespace FlowBlot
{
    public class Flow_00002_inplace
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
            /*FLOW:Flow_00002_inplace - A nested method call source to sink taint propagation:codethreat.flowblot.benchmark:1:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00002_inplace.Sink(Flow_00002_inplace.Source());
        }
    }
}
