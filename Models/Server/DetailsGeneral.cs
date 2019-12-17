using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Server
{
    public class DetailsGeneral
    {
        public string Domain { get; set; }
        public string IpAddress { get; set; }
        public string WebServer { get; set; }
        public string OperatingSystem { get; set; }
        public string CPU { get; set; }
        public string Memory { get; set; }
        public string LastBackup { get; set; }
        public string SiteCount { get; set; }
        public string OnlineSiteCount { get; set; }
        public string Volumes { get; set; }
        public string TotalCapacity { get; set; }
        public string PercentFree { get; set; }
        public string LastCheckDate { get; set; }
        public List<DetailsVolume> VolumeDetails { get; set; }
    }
}
