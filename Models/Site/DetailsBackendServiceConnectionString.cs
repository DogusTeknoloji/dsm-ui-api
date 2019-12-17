using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Site
{
    public class DetailsBackendServiceConnectionString
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string ConnectionString { get; set; }
    }
}
