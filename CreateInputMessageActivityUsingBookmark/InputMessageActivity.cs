using System.Activities;

namespace CreateInputMessageActivityUsingBookmark
{
    public class InputMessageActivity<T> : NativeActivity
    {
        public InArgument<string> BookmarkName { get; set; }
        public OutArgument<T> Result { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            context.CreateBookmark(BookmarkName.Get(context), new BookmarkCallback(OnResumeBookmark));
        }

        public void OnResumeBookmark(NativeActivityContext context, Bookmark bookmark, object value)
        {
            throw new System.NotImplementedException();
        }

        protected override bool CanInduceIdle => true;
    }
}