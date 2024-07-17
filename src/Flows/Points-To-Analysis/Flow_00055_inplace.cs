using FlowBlot.Model;

namespace FlowBlot
{
    public class Flow_00055_inplace
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
            string input = Flow_00055_inplace.Source();

            D a = new D();
            D b = new D();
            b.x = input;

            E e = new E(a);
            e.ReInitialize(b);
            D c = e.Get();

            /*FLOW:Flow_00055_inplace - A basic alias analysis taint propagation:codethreat.flowblot.benchmark:0+:FIND_ISSUE:1:
              *STEP_PATH:ABC
              */
            Flow_00055_inplace.Sink(c.x);
        }
    }
}
