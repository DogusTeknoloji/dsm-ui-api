using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Company
{
    public class DetailsServers
    {
        public int ServerId { get; set; }
        public string ServerName { get; set; }
        public string FullName { get; set; }
        public string IpAddress { get; set; }
        public string OperatingSystem { get; set; }
        public string Environments { get; set; }
        public string ApplicationType { get; set; }
        public string Responsible { get; set; }
        public string Contact { get; set; }
        public string LastBackupDate { get; set; }
    }
}
