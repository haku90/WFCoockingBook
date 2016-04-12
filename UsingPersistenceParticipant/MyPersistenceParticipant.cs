using System;
using System.Activities.Persistence;
using System.Collections.Generic;
using System.Xml.Linq;

namespace UsingPersistenceParticipant
{
    public class MyPersistenceParticipant : PersistenceParticipant
    {
        public string Message { get; set; }
        private  XNamespace _dataNamespace = XNamespace.Get("http://xhinker.com/");

        protected override void CollectValues(out IDictionary<XName, object> readWriteValues, out IDictionary<XName, object> writeOnlyValues)
        {
            readWriteValues = new Dictionary<XName, object>();
            readWriteValues.Add(_dataNamespace.GetName("messageXName"), Message);
            writeOnlyValues = null;
        }

        protected override IDictionary<XName, object> MapValues(IDictionary<XName, object> readWriteValues, IDictionary<XName, object> writeOnlyValues)
        {
            return base.MapValues(readWriteValues, writeOnlyValues);
        }

        protected override void PublishValues(IDictionary<XName, object> readWriteValues)
        {
            Console.WriteLine($"Message: {readWriteValues[_dataNamespace.GetName("messageXName")]}");
        }
    }
}