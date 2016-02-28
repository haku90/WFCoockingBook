using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Threading;

namespace UseBookmark
{

    class Program
    {
        static void Main(string[] args)
        {
            var syncEvent = new AutoResetEvent(false);
            var bookMarkName = "GreetingBookmark";
            var wfApp = new WorkflowApplication(new Workflow1
            {
                BookmarnNameInArg = bookMarkName
            });

            wfApp.Completed = delegate(WorkflowApplicationCompletedEventArgs e)
            {
                syncEvent.Set();
            };

            wfApp.Run();
            wfApp.ResumeBookmark(bookMarkName, Console.ReadLine());
            syncEvent.WaitOne();
        }
    }
}
