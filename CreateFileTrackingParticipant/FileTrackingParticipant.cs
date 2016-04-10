using System;
using System.Activities.Tracking;
using System.IO;

namespace CreateFileTrackingParticipant
{
    public class FileTrackingParticipant : TrackingParticipant
    {
        private string _fileName;

        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            _fileName = $@"C:\logs\{record.InstanceId}.tracking";
            using (var sw = File.AppendText(_fileName)) 
            {
                sw.WriteLine("-----------Tracking Started-----------");
                sw.WriteLine(record.ToString());
                sw.WriteLine("-----------Trakcing End---------------");
            }
        }
    }
}