using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Monitoring
{
    public class ExtendedContact
    {
        public int ExtendedContactId { get; set; }
        public int? ContactId { get; set; }
        public string FullName { get; set; }
        public string EMail { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Department { get; set; }
        public string Unit { get; set; }
        public int? ManagerContactId { get; set; }
        public string Notes { get; set; }
    }
}
