using System.Activities;
using System.IO;
using System.Threading;

namespace CreateFileWriterActivity
{
    public class FileWriterActivity : CodeActivity
    {
        [RequiredArgument]
        public InArgument<string> FileName { get; set; }

        [RequiredArgument]
        public InArgument<string> FileData { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var lines = FileData.Get(context);
            using (var file = new StreamWriter(FileName.Get(context)))
            {
                file.WriteLine(lines);
                Thread.Sleep(5000);
                file.Close();
            }
        }
    }
}