using System;
using System.Activities.Hosting;
using System.Collections.Generic;

namespace UsingCustomizedExtension
{
    public class SimpleExtension : IWorkflowInstanceExtension
    {
        private WorkflowInstanceProxy _instance;

        public IEnumerable<object> GetAdditionalExtensions()
        {
            return null;
        }

        public void SetInstance(WorkflowInstanceProxy instance)
        {
            _instance = instance;
        }

        public void DoSomething()
        {
            Console.WriteLine("Extension is doing something...");
        }
    }
}