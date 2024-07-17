using FlowBlot.Model;

namespace FlowBlot
{
    public class Flow_00056_inplace
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
            string input = Flow_00056_inplace.Source();

            D a = new D();
            a.x = input;
            D b = new D();

            E e = new E(a);
            e.ReInitialize(b);
            D c = e.Get();

            /*FLOW:Flow_00056_inplace - A basic alias analysis FP taint propagation:codethreat.flowblot.benchmark:0:NONE:0:
              *STEP_PATH:ABC
              */
            Flow_00056_inplace.Sink(c.x);
        }
    }
}
