using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Activities.XamlIntegration;
using System.IO;
using System.Text;
using System.Xaml;

namespace ConvertWFInstanceToXML
{

    class Program
    {
        static void Main(string[] args)
        {
            var ab = new ActivityBuilder();
            ab.Implementation = new Sequence
            {
                Activities = { new WriteLine {Text = "Message from Workflow"} }
            };
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var xw = ActivityXamlServices.CreateBuilderWriter(new XamlXmlWriter(sw, new XamlSchemaContext()));
            XamlServices.Save(xw, ab);
            Console.WriteLine(sb.ToString());
        }
    }
}
