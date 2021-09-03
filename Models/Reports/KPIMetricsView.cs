using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Reports
{
    public class KPIMetricsView
    {
        public string Application { get; set; }
        public int Year { get; set; }
        public string Ocak { get; set; }
        public string Subat { get; set; }
        public string Mart { get; set; }
        public string Nisan { get; set; }
        public string Mayis { get; set; }
        public string Haziran { get; set; }
        public string Temmuz { get; set; }
        public string Agustos { get; set; }
        public string Eylul { get; set; }
        public string Ekim { get; set; }
        public string Kasim { get; set; }
        public string Aralik { get; set; }
    }
}
