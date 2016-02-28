using System;
using System.Activities;

namespace UseBookmark
{
    public class MyReadLineActivity : NativeActivity<string>
    {
        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            context.CreateBookmark(BookmarkName.Get(context), new BookmarkCallback(OnResumeBookmark));
        }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        private void OnResumeBookmark(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Result.Set(context, (string) value);
        }
    }
}