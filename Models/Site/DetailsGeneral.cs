namespace DSM.UI.Api.Models.Site
{
    public class DetailsGeneral
    {
        public string MachineName { get; set; }
        public string Name { get; set; }
        public string ApplicationPoolName { get; set; }
        public string PhysicalPath { get; set; }
        public string EnabledProtocols { get; set; }
        public string MaxBandwidth { get; set; }
        public string MaxConnections { get; set; }
        public string LogFileEnabled { get; set; }
        public string LogFileDirectory { get; set; }
        public string LogFormat { get; set; }
        public string LogPeriod { get; set; }
        public string ServerAutoStart { get; set; }
        public string TraceFailedRequestsLoggingEnabled { get; set; }
        public string LastUpdated { get; set; }
        public string DateDeleted { get; set; }
        public string WebConfigLastBackupDate { get; set; }
        public string WebcConfigBackupDirectory { get; set; }
        public string NetFrameworkVersion { get; set; }
        public string SendAlertMAilWhenUnavailable { get; set; }
        public string AppType { get; set; }
    }
}
