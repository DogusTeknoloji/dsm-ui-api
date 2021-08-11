using DSM.Core.Models;
using DSM.UI.Api.Helpers;
using System;

namespace DSM.UI.Api.Models.Site
{
    public class SearchResult : IMappable<Core.Models.Site>
    {
        public long SiteId { get; set; }
        public string Machinename { get; set; }
        public string IpAddress { get; set; }
        public string SiteName { get; set; }
        public string HostName { get; set; }
        public string Port { get; set; }
        public string LogFileDirectory { get; set; }
        public string PhysicalPath { get; set; }
        public string AppType { get; set; }
        public string AppPoolName { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public DateTime DateDeleted { get; set; }

        public IMappable<Core.Models.Site> Map(Core.Models.Site item)
        {
            this.Machinename = item.MachineName;
            this.SiteName = item.Name;
            this.LogFileDirectory = item.LogFileDirectory;
            this.PhysicalPath = item.PhysicalPath;
            this.SiteId = item.SiteId;
            this.AppType = item.AppType;
            this.AppPoolName = item.ApplicationPoolName;
            this.DateLastUpdated = item.LastUpdated;
            this.DateDeleted = item.DateDeleted;
            return this;
        }
    }
}
