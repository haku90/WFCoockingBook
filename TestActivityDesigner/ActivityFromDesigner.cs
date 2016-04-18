using System.Activities;
using System.ComponentModel;

namespace TestActivityDesigner
{
    [Designer(typeof(ActivityDesigner1))]
    public class ActivityFromDesigner : CodeActivity
    {
        public InArgument<string> Text { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Text.Set(context, "Dupa");
        }
    }
}