using System.Collections.Generic;

namespace DSM.UI.Api.Models.AzureDevOps
{
    public class AzurePortalSiteNameWithBindings
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public List<string> Bindings { get; set; }
        public string DefaultHostName { get; set; }
        public string OutboundIpAddresses { get; set; }
        public string ResourceGroup { get; set; }
    }
}