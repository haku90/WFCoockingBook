using System;
using System.Linq;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Activities.Statements;
using System.Threading;

namespace CreateSqlPersistenceStore
{

    class Program
    {
        static void Main(string[] args)
        {
            var sqlPersistenceDbConnectionString = @"Server=.\SQLEXPRESS;Initial Catalog=PersistenceDatabase;Integrated Security=True";

            var sqlWFInstanceStore = new SqlWorkflowInstanceStore(sqlPersistenceDbConnectionString);
            var waitHandler = new AutoResetEvent(initialState: false);
            var wfApp = new WorkflowApplication(new Workflow1());
            wfApp.InstanceStore = sqlWFInstanceStore;
            wfApp.Unloaded = (arg) => { waitHandler.Set(); };
            wfApp.PersistableIdle = (arg) => { return PersistableIdleAction.Unload; };
            wfApp.Run();
            waitHandler.WaitOne();
        }
    }
}
