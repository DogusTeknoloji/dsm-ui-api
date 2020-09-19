using DSM.UI.Api.Helpers;

namespace DSM.UI.Api.Models.User
{
    public class DomainUserInfo : IMappable<User>, IMappable<GetUserModel>
    {
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string MobilePhone { get; set; }
        public string Company { get; set; }
        public string SamAccountName { get; set; }
        public string MailNickName { get; set; }
        public string AccountCreateDate { get; set; }
        public string PasswordLastSet { get; set; }
        public string LastLogonTime { get; set; }
        public string LogonCount { get; set; }
        public string OfficeName { get; set; }
        public string Location { get; set; }
        public string EmployeeId { get; set; }
        public string DateOfHire { get; set; }
        public string Username { get; set; }
        public string ProfileImage { get; set; }


        public int? DomainId { get; set; }

        public IMappable<User> Map(User item)
        {
            this.FullName = item.FullName;
            this.Username = item.Username;
            this.ProfileImage = item.ProfileImage;
            this.DomainId = item.DomainId;
            return this;
        }

        public IMappable<GetUserModel> Map(GetUserModel item)
        {
            this.AccountCreateDate = item.AccountCreateDate;
            this.Company = item.Company;
            this.DateOfHire = item.DateOfHire;
            this.Department = item.Department;
            this.DomainId = item.DomainId;
            this.EmployeeId = item.EmployeeId;
            this.FullName = item.FullName;
            this.LastLogonTime = item.LastLogonTime;
            this.Location = item.Location;
            this.LogonCount = item.LogonCount;
            this.MailNickName = item.MailNickName;
            this.MobilePhone = item.MobilePhone;
            this.OfficeName = item.OfficeName;
            this.PasswordLastSet = item.PasswordLastSet;
            this.ProfileImage = item.ProfileImage;
            this.SamAccountName = item.SamAccountName;
            this.Title = item.Title;
            this.Username = item.Username;

            return this;
        }
    }
}