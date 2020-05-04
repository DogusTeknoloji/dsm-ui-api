using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Helpers.JwtHelpers
{
    public class DecodedTokenStruct
    {
        public string Username { get; set; }
        public string Userrole { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string Issuer { get; set; }
        public DateTime? IssuedAt { get; set; }
    }
}
