using System.Activities;

namespace GuesNumberGameInSequence
{

    class Program
    {
        static void Main(string[] args)
        {
            var workflow1 = new Workflow1();
            WorkflowInvoker.Invoke(workflow1);
        }
    }
}
