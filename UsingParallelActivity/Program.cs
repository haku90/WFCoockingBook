using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace UsingParallelActivity
{

    class Program
    {
        static void Main(string[] args)
        {
            var workflow1 = new Workflow1();
            WorkflowInvoker.Invoke(workflow1);
        }
    }
}
