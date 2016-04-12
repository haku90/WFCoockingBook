using System;
using System.Linq;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Activities.Statements;
using System.Threading;

namespace LoadUpWorkflowFromPersistenceDb
{

    class Program
    {
        private static SqlWorkflowInstanceStore _sqlWorkflowInstanceStore = SetUpSqlPersistenceStore();

        static void Main(string[] args)
        {
            StartAndUnloadInstance();
        }

        private static void StartAndUnloadInstance()
        {
            var waitHandler = new AutoResetEvent(initialState: false);
            var wfApp = new WorkflowApplication(new Workflow1());
            wfApp.InstanceStore = _sqlWorkflowInstanceStore;
            wfApp.PersistableIdle = (e) =>
            {
                return PersistableIdleAction.Unload;
                
            };
            wfApp.Unloaded = (e) => { waitHandler.Set(); };
            var id = wfApp.Id;
            wfApp.Run();
            waitHandler.WaitOne();
            LoadAndCompleteInstance(id);
        }

        private static void LoadAndCompleteInstance(Guid id)
        {
            Console.WriteLine("Press <enter> to load the persisted workflow");
            Console.ReadLine();
            var waitHandler = new AutoResetEvent(initialState: false);
            var wfApp = new WorkflowApplication(new Workflow1());
            wfApp.InstanceStore = _sqlWorkflowInstanceStore;
            wfApp.Unloaded = (workflowApplicationEventArgs) => { waitHandler.Set(); };
            wfApp.Load(id);
            wfApp.Run();
            waitHandler.WaitOne();
        }

        private static SqlWorkflowInstanceStore SetUpSqlPersistenceStore()
        {
            var sqlPersistenceDbConnectionString = @"Server=.\SQLEXPRESS;Initial Catalog=PersistenceDatabase;Integrated Security=True";

            var sqlWFInstanceStore = new SqlWorkflowInstanceStore(sqlPersistenceDbConnectionString);
            return sqlWFInstanceStore;
        }

    }
}
