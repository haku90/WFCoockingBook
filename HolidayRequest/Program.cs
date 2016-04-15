using System.Activities.Tracking;


namespace HolidayRequest
{

    class Program
    {
        static void Main(string[] args)
        {

            //var waitHandler = new AutoResetEvent(false);
            //var bookmarkName = "inputBookmark";
            //var sqlPersistenceDbConnectionString = @"Server=.\SQLEXPRESS;Initial Catalog=PersistenceDatabase;Integrated Security=True";
            //var sqlWFInstanceStore = new SqlWorkflowInstanceStore(sqlPersistenceDbConnectionString);
            //var wfApp = new WorkflowApplication(new HolidayRequestWf()
            //{
            //    bookmarkName = bookmarkName
            //});
            //wfApp.InstanceStore = sqlWFInstanceStore;
            //wfApp.Completed = (arg) => { waitHandler.Set(); };
            //wfApp.Run();
            //wfApp.ResumeBookmark(bookmarkName, Console.ReadLine());
            //waitHandler.WaitOne();
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

            var etwTrackingParticipant = new EtwTrackingParticipant { TrackingProfile = trackingProfile };

        }
    }
}
