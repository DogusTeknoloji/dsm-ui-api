using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Site
{
    public class DetailsBackendServiceEndpoint
    {
        public string EndpointName { get; set; }
        public string Port { get; set; }
        public string EndpointUrl { get; set; }
        public string IsAvailable { get; set; }
        public string LastCheckDate { get; set; }
    }
}
