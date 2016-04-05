using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Threading;

namespace CreateInputMessageActivityUsingBookmark
{

    class Program
    {
        static void Main(string[] args)
        {
            var waitHandler = new AutoResetEvent(false);
            var bkName = "inputBookmark";
            var wfApp = new WorkflowApplication(new TestInputMessageWF
            {
                bookMarkname = bkName
            });
            wfApp.Completed = (arg) => { waitHandler.Set(); };
            wfApp.Run();
            wfApp.ResumeBookmark(bkName, Console.ReadLine());
            waitHandler.WaitOne();
        }
    }
}
