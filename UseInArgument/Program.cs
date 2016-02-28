using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections;
using System.Collections.Generic;

namespace UseInArgument
{

    class Program
    {
        static void Main(string[] args)
        {
            //Activity workflow1 = new Workflow1
            //{
            //    FirstName = "Andrew",
            //    SecondName = "Zhu"
            //};
            //IDictionary<string, object> inputDictionary = new Dictionary<string, object>
            //{
            //    { "FristName", "Andrew"},
            //    {"SecondName", "Zhu" }
            //};
            //WorkflowInvoker.Invoke(new Workflow1(), inputDictionary);

            WorkflowInvoker.Invoke(new WorkflowInCode
            {
                FirstName = "Adam",
                SecondName = "Haku"
            });
        }
    }
}
