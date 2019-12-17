using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace DSM.UI.Api.Helpers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeRoleAttribute(params AuthRoles[] roles)
        {
            Policy = string.Join(",", roles.Select(r => r.ToString()));
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
