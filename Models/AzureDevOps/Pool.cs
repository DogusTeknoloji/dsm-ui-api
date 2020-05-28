using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.AzureDevOps
{
    public class Pool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsHosted { get; set; }
        public string PoolType { get; set; }
        public int? Size { get; set; }
        public bool? IsLegacy { get; set; }
        public object Options { get; set; }

    }
}
