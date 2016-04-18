using System.Activities;
using System.ComponentModel;

namespace MyOwnActivityDesigner
{
    [Designer(typeof(OneStep))]
    public class OneStepActivity : CodeActivity
    {
        public InArgument<string> Name{ get; set; }  

        protected override void Execute(CodeActivityContext context)
        {
            
        }
    }
}