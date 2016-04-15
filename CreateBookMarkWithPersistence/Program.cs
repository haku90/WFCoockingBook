using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CreateBookMarkWithPersistence
{

    class Program
    {
        static void Main(string[] args)
        {
            Document document;
            var idsMap = new Dictionary<Guid, string>();
            var connectionString = @"Server=.\SQLEXPRESS;Initial Catalog=PersistenceDatabase;Integrated Security=True"; ;
            var instanceStore = new SqlWorkflowInstanceStore(connectionString);var waitHandler = new AutoResetEvent(initialState: false);
            var rootAcitivty = new Workflow1();
            var wfapp = new WorkflowApplication(rootAcitivty);
            wfapp.InstanceStore = instanceStore;
            wfapp.Unloaded = (workflowApplicationEventArgs) =>
            {
                 Console.WriteLine("WorkflowApplication has Unloaded\n");
                 waitHandler.Set();
            };
            wfapp.PersistableIdle = (e) => { return PersistableIdleAction.Unload; };
            idsMap.Add(wfapp.Id, "OrderNameBookmark");
           
            wfapp.Run();
            Console.WriteLine($"Host thread: {Thread.CurrentThread.ManagedThreadId}");
            waitHandler.WaitOne();
            
            var name = Console.ReadLine();
            rootAcitivty = new Workflow1();
            wfapp = new WorkflowApplication(rootAcitivty);
            wfapp.InstanceStore = instanceStore;
            
            wfapp.Completed = (workflowApplicationCompletedEventArgs) =>
            {
                document = (Document) workflowApplicationCompletedEventArgs.Outputs["DocumentOut"];
                Console.WriteLine($"\n WorkflowApplication has Completed in the {workflowApplicationCompletedEventArgs.CompletionState}");
            };
            wfapp.Unloaded = (workflowApplicationEventArgs) =>
            {
                Console.WriteLine("WorkflowApplication has Unloaded\n");
                waitHandler.Set();
            };
            wfapp.Load(idsMap.First().Key);
            wfapp.ResumeBookmark("OrderNameBookmark", name);
            Console.WriteLine($"Host thread: { Thread.CurrentThread.ManagedThreadId }");
            //var nameFromActivity = rootAcitivty.outArgument;
            
            waitHandler.WaitOne();
            
        }

       }
}
