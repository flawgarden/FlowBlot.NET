using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00022_inplace
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
            Atlas atlas = new Atlas();
            atlas.name = Flow_00022_inplace.Source();

            Machine machine = Build(atlas);

            string input = ((Atlas)machine).SayName();



            /*FLOW:Flow_00022_inplace - A inter-procedural polymorphic taint propagation:codethreat.flowblot.benchmark:8:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00022_inplace.Sink(input);
        }

        public Machine Build(Machine machine)
        {
            return machine;
        }

    }
}
