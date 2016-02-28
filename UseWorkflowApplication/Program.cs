using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Threading;

namespace UseWorkflowApplication
{

    class Program
    {
        static void Main(string[] args)
        {
            var syncEvent = new AutoResetEvent(false);
            var input = new Dictionary<string, object>
            {
                {"Number1", 123 },
                {"Number2", 456 }
            };
            IDictionary<string, object> output = null;
            var wfApp = new WorkflowApplication(new Workflow1(), input);
            wfApp.Completed =
                delegate (WorkflowApplicationCompletedEventArgs e)
                {
                    Console.WriteLine("Workflow thread id: {0}", Thread.CurrentThread.ManagedThreadId);
                    output = e.Outputs;
                    syncEvent.Set();
                };
            wfApp.Run();
            syncEvent.WaitOne();
            Console.WriteLine(output["Result"].ToString());
            Console.WriteLine("Host thread id {0}", Thread.CurrentThread.ManagedThreadId);

        }
    }
}
