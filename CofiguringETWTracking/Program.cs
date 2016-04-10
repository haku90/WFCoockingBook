using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Activities.Tracking;
using System.Threading;

namespace CofiguringETWTracking
{

    class Program
    {
        static void Main(string[] args)
        {
            var trackingProfile = new TrackingProfile();
            trackingProfile.Queries.Add(new WorkflowInstanceQuery
            {
                States = { "*" }
            });

            trackingProfile.Queries.Add(new ActivityStateQuery
            {
                States = { "*" }
            });

            trackingProfile.Queries.Add(new CustomTrackingQuery
            {
                ActivityName = "*",
                Name = "*"
            });

            var etwTrackingParticipant = new EtwTrackingParticipant {TrackingProfile = trackingProfile};
            var waitHandler = new AutoResetEvent(initialState: false);
            
            var wfApp = new WorkflowApplication(new Workflow1());
            wfApp.Completed = (arg) => { waitHandler.Set(); };
            wfApp.Extensions.Add(etwTrackingParticipant);
            wfApp.Run();
            waitHandler.WaitOne();
        }
    }
}
