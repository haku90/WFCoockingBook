using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace CreateAsyncHttpGetActivity
{

    class Program
    {
        static void Main(string[] args)
        {
            WorkflowInvoker.Invoke(new TestAsyncHttpGetWF());
        }
    }
}
