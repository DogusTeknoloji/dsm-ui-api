using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Company
{
    public class DetailsSites
    {
        public long SiteId { get; set; }
        public string SiteName { get; set; }
        public string PhysicalPath { get; set; }
        public string Domains { get; set; }
        public string State { get; set; }
        public string ServerName { get; set; }
        public string DnsName { get; set; }
        public string AppType { get; set; }
    }
}
