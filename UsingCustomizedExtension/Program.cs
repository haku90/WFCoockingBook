using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Threading;

namespace UsingCustomizedExtension
{

    class Program
    {
        static void Main(string[] args)
        {
            var waitHandler = new AutoResetEvent(initialState: false);
            var wfApp = new WorkflowApplication(new Workflow1());
            wfApp.Unloaded = (e) => { waitHandler.Set(); };
            //wfApp.Extensions.Add(new SimpleExtension()); Dodajemy tu albo nadpisujemy CacheMetadata()
            wfApp.Run();
            waitHandler.WaitOne();
        }
    }
}
