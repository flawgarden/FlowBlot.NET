namespace FlowBlot.Flows_inplace
{
    public class Flow_00041_inplace
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
            string input = "Name: {0}";
            string output = System.String.Format(input, System.Console.ReadLine());

            /*CTSECISSUE:Flow_00041_inplace - A string format taint propagation:codethreat.flowblot.benchmark:3:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00041_inplace.Sink(output);
        }

    }
}
