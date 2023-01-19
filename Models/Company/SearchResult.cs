using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Company
{
    public class SearchResult
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int? ServerCount { get; set; }
        public int? SiteCount { get; set; }
    }
}
