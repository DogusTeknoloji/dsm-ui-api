using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Monitoring
{
    public class Alerts
    {
        public int AlertId { get; set; }
        public string AlertDescription { get; set; }
        public string Action1 { get; set; }
        public string Action2 { get; set; }
        public string Action3 { get; set; }
        public string Action4 { get; set; }
        public string AlertSource { get; set; }
        public string Domain { get; set; }
        public string Notes { get; set; }
    }
}
