using System.Collections.Generic;
using System.Reflection;

namespace FlowBlot
{
    public class Flow_00062_inplace
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

        // This case, in fact, is neither true nor false. 
        // For soundiness, it will be handled as true. 

        public void Run()
        {
            string input = Flow_00062_inplace.Source();

            Assembly flowAssembly = Assembly.LoadFile(@"flow.dll");
            System.Type flowAssemblyType = flowAssembly.GetType("Flow.Fetcher");

            object flowInstance = System.Activator.CreateInstance(flowAssemblyType);

            MethodInfo getMethod = flowAssemblyType.GetMethod("Get");

            List<object> arguments = new List<object>();
            arguments.Add(input);
            arguments.Add(System.String.Empty);

            object output = getMethod.Invoke(flowInstance, arguments.ToArray());

            /*FLOW:Flow_00062_inplace - A reflection taint propagation:codethreat.flowblot.benchmark:7:FIND_ISSUE:1:
             *STEP_PATH:ABC
             */
            Flow_00062_inplace.Sink(output.ToString());
        }
    }
}
