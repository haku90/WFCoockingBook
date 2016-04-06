using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace CreateMySequenceActivity
{

    class Program
    {
        static void Main(string[] args)
        {
            WorkflowInvoker.Invoke(GetTestMySequenceWF());
        }

        static Activity GetTestMySequenceWF()
        {
            return new MySequenceActivity
            {
                Activities =
                {
                    new WriteLine { Text = "WriteLine1" },
                    new WriteLine { Text = "WriteLine2" },
                    new WriteLine {Text = "WriteLine3"}
                }
            };
        }
    }
}
