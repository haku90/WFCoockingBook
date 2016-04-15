using System;
using System.Activities;

namespace CreateBookMarkWithPersistence
{
    public class SubmitOrderName : NativeActivity<Document>
    {
        public InArgument<Document> Document { get; set; }

        protected override bool CanInduceIdle { get { return true; } }

        protected override void Execute(NativeActivityContext context)
        {
            context.CreateBookmark("OrderNameBookmark", new BookmarkCallback(OnBookmarkCallback));
        }

        private void OnBookmarkCallback(NativeActivityContext context, Bookmark bookmark, object value)
        {
            var document = Document.Get(context);
            document.LastUpdate = DateTime.Now;
            document.Author = (string) value;
            Result.Set(context, document);
            
            Console.WriteLine($"Order Name is {(string)value}");
        }
    }
}