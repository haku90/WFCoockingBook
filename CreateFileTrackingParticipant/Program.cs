using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Activities.Tracking;
using System.Threading;

namespace CreateFileTrackingParticipant
{

    class Program
    {
        static void Main(string[] args)
        {
            var fileTrackingProfile = new TrackingProfile();
            fileTrackingProfile.Queries.Add(new WorkflowInstanceQuery
            {
                States = { "*" }
            });

            fileTrackingProfile.Queries.Add(new ActivityStateQuery
            {
                States = { ActivityStates.Executing, ActivityStates.Closed}
            });

            var fileTrackingParticipaint = new FileTrackingParticipant();
            fileTrackingParticipaint.TrackingProfile = fileTrackingProfile;
            var waitHandler = new AutoResetEvent(initialState: false);
            var wfApp = new WorkflowApplication(new Workflow1());
            wfApp.Unloaded = (wfAppEventArg) => { waitHandler.Set(); };
            wfApp.Extensions.Add(fileTrackingParticipaint);
            wfApp.Run();
            waitHandler.WaitOne();
        }
    }
}
