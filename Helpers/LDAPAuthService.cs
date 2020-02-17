using DSM.UI.Api.Models.User;
using DSM.UI.Api.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace DSM.UI.Api.Helpers
{
    public static class LDAPAuthService
    {
        public const string MAILREGEXPATTERN = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public static DomainUserInfo AuthenticateActiveDirectory(IEnumerable<Domain> domains, string UserName, string Password)
        {
            DomainUserInfo userInfo = new DomainUserInfo();
            try
            {
                if (domains.Count() < 1) return null;
                foreach (Domain domain in domains)
                {
                    DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain.DomainName, UserName, Password);
                    if (entry == null) continue;

                    string samAccount = UserName.Split('@')[0];
                    byte[] imageBytes = GetThumbnailPhoto(samAccount);

                    if (imageBytes != null)
                    {
                        string base64String = Convert.ToBase64String(imageBytes);
                        userInfo.ProfileImage = "data:image/png;base64," + base64String;
                    }

                    object nativeObject = entry.NativeObject;
                    userInfo.DomainId = domain.DomainId;
                    userInfo.FullName = GetFullname(UserName);
                    userInfo.Username = UserName;
                    userInfo.Password = Password;

                    return userInfo;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
        public static DomainUserInfo GetCurrentUser()
        {
            DomainUserInfo userInfo = null;
            // set up domain context
            using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain))
            {
                // find current user
                UserPrincipal user = UserPrincipal.Current;
                if (user == null) return userInfo;

                userInfo = new DomainUserInfo
                {
                    Username = user.EmailAddress,
                    FullName = user.DisplayName,
                };

                DirectoryEntry de = user.GetUnderlyingObject() as DirectoryEntry;
                if (de == null) return userInfo;
                if (de.Properties["thumbnailPhoto"] == null) return userInfo;
                try
                {
                    byte[] userImage = de.Properties["thumbnailPhoto"].Value as byte[];

                    if (userImage != null)
                    {
                        byte[] imageBytes = (byte[])userImage;
                        string base64String = Convert.ToBase64String(imageBytes);
                        userInfo.ProfileImage = base64String;
                    }
                }
                catch (Exception)
                {
                }
            }
            return userInfo;
        }
        public static DomainUserHolder ValidateUser(string username, string password, IUserService service)
        {
            DomainUserHolder holder = new DomainUserHolder();
            User user = service.GetByUserName(username);
            //if format is valid for LDAP domains.
            if (!Regex.IsMatch(username, MAILREGEXPATTERN)) return null;

            // if exists, use user's previously saved domain info otherwise get all domains to find valid domain.
            List<Domain> ldapDomains = user == null ? service.GetDomainList() : new List<Domain> { user.Domain };

            string domainName = GetDomainNameFromUsername(username);
            // Create new minified list by which item contains contosodomain in domainlist
            ldapDomains = ldapDomains.Where(x => x.DomainName.Contains(domainName)).ToList();
            // Validate user in domains in the list, if found return domain id else return -1
            holder.User = user;
            holder.DomainUser = AuthenticateActiveDirectory(ldapDomains, username, password);
            return holder;
        }
        private static string GetDomainNameFromUsername(string username)
        {
            //Split by @ and take the second one In Example: username@contoso-domain.com take contoso-domain.com
            string domainName = username.Split('@')[1];
            //Replace not allowed '-' char to empty literal; So contoso-domain.com -> contosodomain.com
            domainName = domainName.Replace("-", "");
            // Split by '.' and take first part of string; So contosodomain.com -> contosodomain
            domainName = domainName.Split('.')[0];
            return domainName;
        }
        private static string GetFullname(string username)
        {
            return username.Split('@')[0];
        }
        public static byte[] GetThumbnailPhoto(string userName)
        {
            using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain))
            {
                using (UserPrincipal userPrincipal = new UserPrincipal(principalContext))
                {
                    userPrincipal.SamAccountName = userName;
                    using (PrincipalSearcher principalSearcher = new PrincipalSearcher())
                    {
                        principalSearcher.QueryFilter = userPrincipal;
                        Principal principal = principalSearcher.FindOne();
                        if (principal != null)
                        {
                            DirectoryEntry directoryEntry = (DirectoryEntry)principal.GetUnderlyingObject();
                            PropertyValueCollection collection = directoryEntry.Properties["thumbnailPhoto"];

                            if (collection.Value != null && collection.Value is byte[])
                            {
                                byte[] thumbnailInBytes = (byte[])collection.Value;
                                return thumbnailInBytes;
                            }
                        }
                        return null;
                    }
                }
            }
        }

    }
}