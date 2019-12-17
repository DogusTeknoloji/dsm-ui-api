using DSM.UI.Api.Helpers;
using System.ComponentModel.DataAnnotations;

namespace DSM.UI.Api.Models.User
{
    public class AuthenticateModel : IMappable<User>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public IMappable<User> Map(User item)
        {
            this.Username = item.Username;
            this.Password = item.Password;
            return this;
        }
    }
}
