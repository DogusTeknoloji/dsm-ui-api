using System;
namespace DSM.UI.Api.Models.Reports
{
    public class OverallDiskStatusItem
    {
        public string CompanyName { get; set; }
        public string ServerName { get; set; }
        public DateTime? LastCheckDate { get; set; }
        public string VolumeName { get; set; }
        public string DiskCapacity { get; set; }
        public string UsedDiskSpace { get; set; }
        public string FreeDiskSpace { get; set; }
        public int? FreePercentage { get; set; }
        public int ServerId { get; set; }
        public string Responsible { get; set; }
    }
}
