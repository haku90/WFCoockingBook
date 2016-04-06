using System.Activities;
using System.Collections.ObjectModel;

namespace CreateMySequenceActivity
{
    public class MySequenceActivity : NativeActivity
    {
        public Collection<Activity> Activities { get; set; }
        private int _activityCounter;

        public MySequenceActivity()
        {
            Activities = new Collection<Activity>();
        }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            foreach (var activity in Activities)
            {
                metadata.AddImplementationChild(activity);
            }
        }

        protected override void Execute(NativeActivityContext context)
        {
            ScheduleActivities(context);
        }

        private void ScheduleActivities(NativeActivityContext context)
        {
            if (_activityCounter < Activities.Count)
            {
                context.ScheduleActivity(Activities[_activityCounter++], OnActivityCompleted);
            }
        }

        private void OnActivityCompleted(NativeActivityContext context, ActivityInstance completedinstance)
        {
            ScheduleActivities(context);
        }
    }
}