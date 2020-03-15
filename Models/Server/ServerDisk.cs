using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Server
{
    public class ServerDisk
    {
        public long DiskId { get; set; }
        public string DiskName { get; set; }
        public int? DiskCapacity { get; set; }
        public int? DiskFreeSpace { get; set; }
        public int ServerId { get; set; }
        public virtual Server Server { get; set; }
    }
}
