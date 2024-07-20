using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00021_inplace
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
            Machine machine = new Robot();
            Robot robot = (Robot)machine;
            robot.name = Flow_00021_inplace.Source();

            string input = robot.name;

            /*FLOW:Flow_00021_inplace - A polymorphic taint propagation:codethreat.flowblot.benchmark:5:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00021_inplace.Sink(input);
        }

    }
}
