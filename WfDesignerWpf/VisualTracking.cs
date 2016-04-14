using System;
using System.Activities;
using System.Activities.Debugger;
using System.Activities.Presentation;
using System.Activities.Presentation.Debug;
using System.Activities.Presentation.Services;
using System.Activities.Tracking;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Threading;

namespace WfDesignerWpf
{
    public class VisualTracking : TrackingParticipant
    {
        private WorkflowDesigner _workflowDesigner;
        private DebuggerService _debuggerService;
        private Dictionary<object, SourceLocation> _sourceLocationMaps;
        private Dictionary<string, Activity> _idActivityMaps;
        private Activity _tempActivity;
         
        public VisualTracking(WorkflowDesigner workflowDesigner)
        {
            _workflowDesigner = workflowDesigner;
            _debuggerService =  _workflowDesigner.DebugManagerView as DebuggerService;
            var trakcingProfile = new TrackingProfile();
            trakcingProfile.Queries.Add(new ActivityStateQuery {ActivityName = "*", States = { ActivityStates.Executing}, Variables = { "*" }, Arguments = { "*" }});
            TrackingProfile = trakcingProfile;
            _sourceLocationMaps = GetSourceLocationMap();
            _idActivityMaps = GetIdActivityMaps();
        }
        
        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            var activitySteateRecord = record as ActivityStateRecord;
            if (activitySteateRecord == null)
            {
                return;
            }
            if (!_idActivityMaps.ContainsKey(activitySteateRecord.Activity.Id))
            {
                return;
            }
            _workflowDesigner.View.Dispatcher.Invoke(DispatcherPriority.Render, (Action) (() =>
            {
                _tempActivity = _idActivityMaps[activitySteateRecord.Activity.Id];
                _workflowDesigner.DebugManagerView.CurrentLocation = _sourceLocationMaps[_tempActivity];
                Thread.Sleep(1000);
            }));
        }

        private Dictionary<object, SourceLocation> GetSourceLocationMap()
        {
            var runtimeDebug = new Dictionary<object, SourceLocation>();
            var debugDebug = new Dictionary<object, SourceLocation>();
            var fileItem = _workflowDesigner.Context.Items.GetValue(typeof (WorkflowFileItem)) as WorkflowFileItem;
            var debugActivity = GetDebugActivity();
            var runtTimeActivity = GetRuntimeActivity();
            SourceLocationProvider.CollectMapping(runtTimeActivity, debugActivity, runtimeDebug, fileItem.LoadedFile);
            SourceLocationProvider.CollectMapping(debugActivity, debugActivity, debugDebug, fileItem.LoadedFile);
            _debuggerService.UpdateSourceLocations(debugDebug);
            return runtimeDebug;
        }

        private Dictionary<string, Activity> GetIdActivityMaps()
        {
            var idToActivity = new Dictionary<string, Activity>();
            foreach (Activity activity in _sourceLocationMaps.Keys)
            {
                idToActivity.Add(activity.Id, activity);
            }
            return idToActivity;
        }

        private Activity GetDebugActivity()
        {
            var modelService = _workflowDesigner.Context.Services.GetService<ModelService>();
            var debugTree = modelService.Root.GetCurrentValue() as IDebuggableWorkflowTree;
            return debugTree != null ? debugTree.GetWorkflowRoot() : null;
        }

        private Activity GetRuntimeActivity()
        {
            _workflowDesigner.Flush();
            var stringReader = new StringReader(_workflowDesigner.Text);
            var root = ActivityXamlServices.Load(stringReader);
            WorkflowInspectionServices.CacheMetadata(root);
            var list = WorkflowInspectionServices.GetActivities(root).GetEnumerator();
            list.MoveNext();
            var runtimeActivity = list.Current;
            return runtimeActivity;

        }
    }
}