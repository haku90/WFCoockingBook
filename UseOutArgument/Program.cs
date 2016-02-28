using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;

namespace UseOutArgument
{

    class Program
    {
        static void Main(string[] args)
        {
            Activity workflow1 = new Workflow1();
            IDictionary<string, object>output = WorkflowInvoker.Invoke(workflow1);
            Console.WriteLine(output["OutMessage"]);
        }
    }
}
