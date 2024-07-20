namespace FlowBlot.Flows_inplace
{
    public class Flow_00011_inplace
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
            string input = Flow_00011_inplace.Source();
            string colored = Color(input);

            /*FLOW:Flow_00011_inplace - A return value taint propagation:codethreat.flowblot.benchmark:6:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00011_inplace.Sink(colored);
        }

        public string Color(string input)
        {
            return input;
        }
    }
}
