using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;

namespace UseInOutArgument
{

    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string, object> input = new Dictionary<string, object>
            {
                {"InOutMessage", "Now, I am InMessage" }
            };
            Activity workflow1 = new Workflow1();
            var output = WorkflowInvoker.Invoke(workflow1, input);
            Console.WriteLine(output["InOutMessage"]);
        }
    }
}
