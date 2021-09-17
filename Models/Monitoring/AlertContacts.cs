using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Monitoring
{
    public class AlertContacts
    {

        public int AlertContactId { get; set; }
        public int AlertId { get; set; }
        public int ContactId { get; set; }
        public int ExtendedContactId { get; set; }
        public int Priority { get; set; }
        public string ActionType { get; set; }
    }
}
