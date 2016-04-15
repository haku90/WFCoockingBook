using System.Activities;

namespace HolidayRequest.Activities
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
            Result.Set(context, (T)value);
        }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }
    }
}