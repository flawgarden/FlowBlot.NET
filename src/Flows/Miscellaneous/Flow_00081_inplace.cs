namespace FlowBlot.Flows_inplace
{
    public class Flow_00081_inplace
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
            string input = Flow_00081_inplace.Source();            
            RunMe(string.Empty, input, null, null);
        }

        private void RunMe(string one, params string[] arguments)
        {
            if (arguments != null)
            {
                /*FLOW:Flow_00081_inplace - A dynamic number of arguments method call:codethreat.flowblot.benchmark:5:FIND_ISSUE:1:
                    *STEP_PATH:ABC
                    */
                Flow_00081_inplace.Sink(arguments[0]);
            }
        }
    }
}
