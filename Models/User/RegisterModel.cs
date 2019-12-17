using DSM.UI.Api.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.User
{
    public class RegisterModel : IMappable<User>
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public int? DomainId { get; set; }

        public IMappable<User> Map(User item)
        {
            this.FullName = item.FullName;
            this.Username = item.Username;
            this.Password = item.Password;
            this.DomainId = item.DomainId;
            return this;
        }
    }
}
