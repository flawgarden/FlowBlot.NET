namespace FlowBlot.Flows_inplace
{
    public class Flow_00025_inplace
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
            string input = Flow_00025_inplace.Source();

            /*FLOW:Flow_00025_inplace - A multi sink taint propagation:codethreat.flowblot.benchmark:3:FIND_ISSUE:1:
             */
            Flow_00025_inplace.Sink(input);

            new Flow_00026_inplace().Run(input);
        }
    }
}
