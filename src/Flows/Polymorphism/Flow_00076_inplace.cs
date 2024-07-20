using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00076_inplace
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
            Robot robot = new Robot();
            robot.Pause();
        }

        public void PauseMachine(Machine machine)
        {
            machine.Pause();
            string name = machine.SayName();

            /*FLOW:Flow_00076_inplace - An inheritance based taint propagation:codethreat.flowblot.benchmark:0:NONE:0:
             *STEP_PATH:ABC
             */
            Flow_00076_inplace.Sink(name);
        }
    }
}
