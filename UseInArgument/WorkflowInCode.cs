using System.Activities;
using System.Activities.Statements;

namespace UseInArgument
{
    public class WorkflowInCode : Activity
    {
        public InArgument<string> FirstName { get; set; }
        public InArgument<string> SecondName { get; set; }

        public WorkflowInCode()
        {
            Implementation = () => new Sequence
            {
                Activities =
                {
                    new WriteLine
                    {
                        Text = new InArgument<string>(activityContext=> "My name is " + FirstName.Get(activityContext))
                    },
                    new WriteLine
                    {
                        Text = new InArgument<string>(activityContext=>SecondName.Get(activityContext))
                    }
                }
            };
        }
    }
}