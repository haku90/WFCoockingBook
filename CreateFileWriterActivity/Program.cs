using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace CreateFileWriterActivity
{

    class Program
    {
        static void Main(string[] args)
        {
            //WorkflowInvoker.Invoke(new TestFileWriterWF());
            WorkflowInvoker.Invoke(CodeStyleWorkFlow());
        }

        private static Activity CodeStyleWorkFlow()
        {
            var wf = new Sequence
            {
                Activities =
                {
                    new WriteLine {Text = "Start..."},
                    new FileWriterActivity
                    {
                        FileName = "Test.txt",
                        FileData = "Text content"
                    },
                    new WriteLine {Text = "End..."}
                }
            };
            return wf;
        }
    }
}
