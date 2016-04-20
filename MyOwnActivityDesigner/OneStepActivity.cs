using System;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Presentation.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyOwnActivityDesigner.Annotations;
using UseInArgument;

namespace MyOwnActivityDesigner
{
    [Designer(typeof(OneStep))]
    public class OneStepActivity : NativeActivity
    {
        public InArgument<string> Name{ get; set; }
        public InArgument<object> Document { get; set; }
        public Type DocumentType { get; set; }

        public OneStepActivity()
        {
            
        }

        protected override void Execute(NativeActivityContext context)
        {
            
        }

        private void ExecuteChangeType(object obj)
        {
            if (DocumentType == null)
                return;
            var properties = DocumentType.GetProperties();
            foreach (var property in properties)
            {
                
            }
        }

        
    }
}