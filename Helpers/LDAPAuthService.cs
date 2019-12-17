using DSM.UI.Api.Models.User;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Helpers
{
    public static class LDAPAuthService
    {
        public const string MAILREGEXPATTERN = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public static int AuthenticateActiveDirectory(IEnumerable<Domain> domains, string UserName, string Password)
        {
            try
            {
                if (domains.Count() < 1) return -1;
                foreach (Domain domain in domains)
                {
                    try
                    {

                        DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain.DomainName, UserName, Password);
                        object nativeObject = entry.NativeObject;
                        return domain.DomainId;
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
