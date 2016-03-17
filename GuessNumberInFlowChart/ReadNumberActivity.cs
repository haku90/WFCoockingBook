using System;
using System.Activities;

namespace GuessNumberInFlowChart
{
    public class ReadNumberActivity : CodeActivity
    {
        public OutArgument<int> OutNumber { get; set; }
        
        protected override void Execute(CodeActivityContext context)
        {
            OutNumber.Set(context, Int32.Parse(Console.ReadLine()));
        }
    }
}