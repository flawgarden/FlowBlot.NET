
namespace FlowBlot.Flows_inplace
{
    public class Flow_00018_inplace
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
            string input = Flow_00018_inplace.Source();

            string output = Nest(Nest(Nest(Nest(Nest(Nest(Nest(input)))))));

            /*FLOW:Flow_00018_inplace - A deep nested call taint propagation:codethreat.flowblot.benchmark:16:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00018_inplace.Sink(output);
        }

        public string Nest(string input)
        {
            return input;
        }

    }
}
