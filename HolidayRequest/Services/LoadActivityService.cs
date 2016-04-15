using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.IO;
using System.Text;

namespace HolidayRequest.Services
{
    internal class LoadActivityService
    {
        internal Activity LoadActivityFromFile(string filePath)
        {
            var actualLine = String.Empty;
            var xamlWFString = new StringBuilder();
            var xamlStreamReader = new StreamReader(filePath);

            do
            {
                actualLine = xamlStreamReader.ReadLine();
                if (!String.IsNullOrEmpty(actualLine))
                {
                    xamlWFString.Append(actualLine);
                }
            } while (!String.IsNullOrEmpty(actualLine));
            var activity = ActivityXamlServices.Load(new StringReader(xamlWFString.ToString()));
            return activity;
        }

        internal Activity LoadActivityFromString(string xamlActivity)
        {
            var activity = ActivityXamlServices.Load(xamlActivity);
            return activity;
        }
    }
}