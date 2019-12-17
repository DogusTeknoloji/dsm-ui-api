using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Server
{
    public class DetailsVolume
    {
        public string VolumeName { get; set; }
        public string FreeSpace { get; set; }
        public string UsedSpace { get; set; }
        public string TotalCapacity { get; set; }
        public string FreePercent { get; set; }
    }
}
