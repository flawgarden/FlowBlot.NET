namespace FlowBlot.Flows_inplace
{
    public class Flow_00040_inplace
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
            float x = 5.0f;
            if(x > 6.5f)
            {
                string input = System.String.Empty;
            }
        }

        private void NeverCalled()
        {
            string input = Flow_00040_inplace.Source();

            /*FLOW:Flow_00040_inplace - An unreachable taint flow:codethreat.flowblot.benchmark:3:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00040_inplace.Sink(input);
        }
    }
}
