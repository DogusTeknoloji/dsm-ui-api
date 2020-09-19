using DSM.UI.Api.Models.User;
using DSM.UI.Api.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace DSM.UI.Api.Helpers
{
    public static class LDAPAuthService
    {
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
        private static Dictionary<string, string> GetUserInfoAll(Domain domain, string userName)
        {
            PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, domain.DomainName);
            UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, userName);

            DirectoryEntry userEntry = (DirectoryEntry)userPrincipal.GetUnderlyingObject();
            DirectorySearcher upSearcher = new DirectorySearcher(userEntry);
            SearchResultCollection results = upSearcher.FindAll();
            ResultPropertyCollection rpc = results[0].Properties;

            Dictionary<string, string> userInfo = new Dictionary<string, string>();
            foreach (string rp in rpc.PropertyNames)
            {
                userInfo.Add(rp.ToString(), rpc[rp][0].ToString());
            }

            return userInfo;
        }
        private static DomainUserInfo GetUserInfo(Domain domain, string userName)
        {
            Dictionary<string, string> userInfoDict = GetUserInfoAll(domain, userName);

            DomainUserInfo userInfo = new DomainUserInfo
            {
                AccountCreateDate = userInfoDict[UserProperties.USER_CREATE_DATE],
                Company = userInfoDict[UserProperties.USER_COMPANY],
                DateOfHire = userInfoDict[UserProperties.USER_DATE_OF_HIRE],
                Department = userInfoDict[UserProperties.USER_DEPARTMENT],
                EmployeeId = userInfoDict[UserProperties.USER_EMPLOYEE_ID],
                FullName = $"{userInfoDict[UserProperties.USER_NAME]} {userInfoDict[UserProperties.USER_SURNAME]}",
                LastLogonTime = userInfoDict[UserProperties.USER_LAST_LOGON_TIME],
                Location = userInfoDict[UserProperties.USER_LOCATION],
                LogonCount = userInfoDict[UserProperties.USER_LOGON_COUNT],
                MailNickName = userInfoDict[UserProperties.USER_MAIL_NICK_NAME],
                MobilePhone = userInfoDict[UserProperties.USER_MOBILE_PHONE],
                OfficeName = userInfoDict[UserProperties.USER_OFFICE_NAME],
                PasswordLastSet = userInfoDict[UserProperties.USER_PASSWORD_LAST_SET],
                SamAccountName = userInfoDict[UserProperties.USER_SAM_ACCOUNT_NAME],
                Title = userInfoDict[UserProperties.USER_TITLE]
            };

            return userInfo;
        }
        private static void HandleLdapError(LdapException lex)
        {
            string errorMessage = lex.ServerErrorMessage;
            string errorCode = errorMessage.Substring(76, 4).Trim();

            throw errorCode switch
            {
                LDAPErrorCode.INVALID_USER_OR_PASSWORD => new Exception("Invalid User or Password"),
                LDAPErrorCode.PASSWORD_EXPIRED => new Exception("Password Expired"),
                LDAPErrorCode.ACCOUNT_LOCKED => new Exception("Account Locked"),
                LDAPErrorCode.ACCOUNT_DISABLED => new Exception("Account Disabled"),
                LDAPErrorCode.USER_NOT_FOUND => new Exception("User Not Found"),
                _ => new Exception("Unknown Error Code"),
            };
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
        public static string MAILREGEXPATTERN
        {
            get
            {
                return @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            }
        }
        public static DomainUserInfo AuthenticateActiveDirectory(IEnumerable<Domain> domains, string UserName, string Password, out string message)
        {
            DomainUserInfo userInfo = null;
            message = "Success";
            try
            {
                if (domains.Count() < 1) return null;
                foreach (Domain domain in domains)
                {
                    try
                    {
                        LdapConnection ldapConnection = new LdapConnection(domain.DomainName);
                        NetworkCredential credentials = new NetworkCredential(UserName, Password);
                        ldapConnection.Credential = credentials;
                        ldapConnection.Bind();
                    }
                    catch (LdapException lexc)
                    {
                        HandleLdapError(lexc);
                    }

                    userInfo = GetUserInfo(domain, UserName);
                    byte[] imageBytes = GetThumbnailPhoto(userInfo.SamAccountName);

                    if (imageBytes != null)
                    {
                        string base64String = Convert.ToBase64String(imageBytes);
                        userInfo.ProfileImage = "data:image/png;base64," + base64String;
                    }

                    userInfo.DomainId = domain.DomainId;
                    userInfo.Username = UserName;

                    return userInfo;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
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
                string samAccount = user.EmailAddress.Split('@')[0];
                byte[] imageBytes = GetThumbnailPhoto(samAccount);

                if (imageBytes != null)
                {
                    string base64String = Convert.ToBase64String(imageBytes);
                    userInfo.ProfileImage = "data:image/png;base64," + base64String;
                }
            }
            return userInfo;
        }
        public static DomainUserHolder ValidateUser(string username, string password, IUserService service, out string message)
        {
            DomainUserHolder holder = new DomainUserHolder();
            User user = service.GetByUserName(username);
            //if format is valid for LDAP domains.
            message = "Success";
            if (!Regex.IsMatch(username, MAILREGEXPATTERN)) return null;

            // if exists, use user's previously saved domain info otherwise get all domains to find valid domain.
            List<Domain> ldapDomains = user == null ? service.GetDomainList() : new List<Domain> { user.Domain };

            string domainName = GetDomainNameFromUsername(username);
            // Create new minified list by which item contains contosodomain in domainlist
            ldapDomains = ldapDomains.Where(x => x.DomainName.Contains(domainName)).ToList();
            // Validate user in domains in the list, if found return domain id else return -1
            holder.DomainUser = AuthenticateActiveDirectory(ldapDomains, username, password, out message);

            holder.User = MapHelper.Map<GetUserModel, DomainUserInfo>(holder.DomainUser);
            holder.User.Id = user.Id;
            holder.User.Role = user.Role;

            return holder;
        }


        private struct UserProperties
        {
            public const string USER_TITLE = "title";
            public const string USER_DEPARTMENT = "department";
            public const string USER_MOBILE_PHONE = "mobile";
            public const string USER_COMPANY = "company";
            public const string USER_SAM_ACCOUNT_NAME = "samaccountname";
            public const string USER_MAIL_NICK_NAME = "mailnickname";
            public const string USER_SURNAME = "sn";
            public const string USER_NAME = "givenname";
            public const string USER_CREATE_DATE = "whencreated";
            public const string USER_PASSWORD_LAST_SET = "pwdlastset";
            public const string USER_LAST_LOGON_TIME = "lastlogontimestamp";
            public const string USER_LOGON_COUNT = "logoncount";
            public const string USER_OFFICE_NAME = "physicaldeliveryofficename";
            public const string USER_LOCATION = "userlocation";
            public const string USER_EMPLOYEE_ID = "employeeid";
            public const string USER_DISPLAY_NAME = "kymdisplayname";
            public const string USER_DATE_OF_HIRE = "kymdateofhire";
        }
        private struct LDAPErrorCode
        {
            public const string INVALID_USER_OR_PASSWORD = "52e";
            public const string ACCOUNT_LOCKED = "775";
            public const string USER_NOT_FOUND = "525";
            public const string NOT_PERMITTED_AT_THIS_TIME = "530";
            public const string NOT_PERTMITTED_AT_THIS_WORKSTATION = "531";
            public const string PASSWORD_EXPIRED = "532";
            public const string ACCOUNT_DISABLED = "533";
        }
    }
}