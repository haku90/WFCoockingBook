using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace CreateFileWriterActivity
{

    class Program
    {
        static void Main(string[] args)
        {
            WorkflowInvoker.Invoke(new TestFileWriterWF());
        }
    }
}
