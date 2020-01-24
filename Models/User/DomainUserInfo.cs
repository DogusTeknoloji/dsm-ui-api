using DSM.UI.Api.Helpers;

namespace DSM.UI.Api.Models.User
{
    public class DomainUserInfo : IMappable<User>
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string ProfileImage { get; set; }
        public string Password { get; set; }
        public int? DomainId { get; set; }

        public IMappable<User> Map(User item)
        {
            this.FullName = item.FullName;
            this.Username = item.Username;
            this.ProfileImage = item.ProfileImage;
            this.DomainId = item.DomainId;
            this.Password = item.Password;
            return this;
        }
    }
}
