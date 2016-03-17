using System.Activities;
using System.Activities.Statements;

namespace UsingInvokeMethodActivityInCode
{
    public class InvokeMethodActivity
    {
        public static Activity CreateInvokeMethodWf()
        {
            var test = new TestClass();
            var resultValue = new Variable<string>();
            return new Sequence
            {
                Variables = { resultValue },
                Activities =
                {
                    new WriteLine { Text = "...Invoke void Method()" },
                    new InvokeMethod
                    {
                      TargetObject  = new InArgument<TestClass>(aec => test),
                      MethodName = "Method"
                    },
                    new WriteLine(){ Text="...Invoke void Method(string"+"message1,string message2)"},
                    new InvokeMethod(){
                        TargetObject = new InArgument<TestClass>(aec => test),
                        MethodName ="Method",
                        Parameters =
                        {
                            new InArgument<string>("This ismessage1"),
                            new InArgument<string>("This ismessage2")
                        }
                    }
                }
            };
        }
    }
}