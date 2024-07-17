using FlowBlot.Model;

namespace FlowBlot.Flows_inplace
{
    public class Flow_00043_inplace
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
            Blot blot = new Blot();
            string input = Flow_00043_inplace.Source();
            blot.Name = input;
            Method1(blot);

            /*FLOW:Flow_00043_inplace - A model pass-through taint propagation:codethreat.flowblot.benchmark:16:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00043_inplace.Sink(blot.Name);
        }

        public void Method1(System.Object myobj)
        {
            // log myobj
            string s = Method2(myobj);
        }

        public string Method2(System.Object myobj)
        {
            return System.String.Format("LOG {0}/{1}", myobj.ToString(), "");
        }
    }
}
