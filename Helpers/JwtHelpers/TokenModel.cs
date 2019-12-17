using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Helpers.JwtHelpers
{
    public class TokenModel
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Role { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
