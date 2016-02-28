using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.IO;
using System.Text;

namespace LoadUpWorkflowFromXML
{

    class Program
    {
        static void Main(string[] args)
        {
            //var filePath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            //var directory = Path.GetDirectoryName(filePath);
            var fileName = "Workflow1.xaml";
            var filePath = String.Format("{0}\\{1}", Environment.CurrentDirectory, fileName);
            var tempString = String.Empty;

            var xamlWFString = new StringBuilder();
            var xamlStreamReader = new StreamReader(filePath);

            do
            {
                tempString = xamlStreamReader.ReadLine();
                if (!String.IsNullOrEmpty(tempString))
                {
                    xamlWFString.Append(tempString);
                }
            } while (!String.IsNullOrEmpty(tempString));



            var wfInstance = ActivityXamlServices.Load(new StringReader(xamlWFString.ToString()));
            WorkflowInvoker.Invoke(wfInstance);
        }
    }
}
