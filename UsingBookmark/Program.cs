using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Threading;

namespace UsingBookmark
{

    class Program
    {
        static void Main(string[] args)
        {
            var waitHandler = new AutoResetEvent(false);
            var bookmarkName = "inputBookmark";
            var wfApp = new WorkflowApplication(new TestInputMessageWF()
            {
                bookmarkName = bookmarkName
            });

            wfApp.Completed = (arg) => { waitHandler.Set(); };
            wfApp.Run();
            wfApp.ResumeBookmark(bookmarkName, Console.ReadLine());
            waitHandler.WaitOne();
            
        }
    }
}
