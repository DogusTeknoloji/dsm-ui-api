namespace DSM.UI.Api.Models.Reports
{
    public class ScheduledJobListDTO
    {
        public string JobDescription { get; set; }
        public string Owner { get; set; }
        public string HostType { get; set; }
        public string HostName { get; set; }
        public string StartTime { get; set; }
        public string RepeatTime { get; set; }
        public string Interval { get; set; }
        public string JobName { get; set; }
    }
}
