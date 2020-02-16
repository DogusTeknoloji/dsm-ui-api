using SvrModel = DSM.UI.Api.Models.Server;
namespace DSM.UI.Api.Models.Reports
{
    public class ScheduledJobItem
    {
        public string ObjectName { get; set; }
        public string Title { get; set; }
        public string Owner { get; set; }
        public string JobContent { get; set; }
        public string HostType { get; set; }
        public string Host { get; set; }
        public string EstimatedRuntime { get; set; }
        public string TypeOfParent { get; set; }
        public string ParentWorkFlow { get; set; }
        public string CalendarInWorkFlow { get; set; }
        public string CalendarKeywordInWorkflow { get; set; }
        public string Schedule { get; set; }
        public string StartTime { get; set; }
        public string CalendarKeywordInSchedule { get; set; }
        public string CalendarInSchedule { get; set; }
        public string TimeEvent { get; set; }
        public string Interval { get; set; }
        public string EarliestStartTime { get; set; }
        public string Version { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Modified { get; set; }

    }
}
