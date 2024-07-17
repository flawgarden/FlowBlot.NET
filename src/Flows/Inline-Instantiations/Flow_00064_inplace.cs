namespace FlowBlot
{
    public class Flow_00064_inplace
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
            string input = Flow_00064_inplace.Source();

            string [] anArray = new string []
            {
                input,
                System.String.Empty,
                System.String.Empty
            };

            /*FLOW:Flow_00064_inplace - An inline Array definition taint propagation:codethreat.flowblot.benchmark:5:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00064_inplace.Sink(anArray[0]);
        }
    }
}
