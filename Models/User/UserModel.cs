using DSM.UI.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.User
{
    public class UserModel : IMappable<User>
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Domain { get; set; }

        public IMappable<User> Map(User item)
        {
            this.Id = item.Id;
            this.Fullname = item.FullName;
            this.Username = item.Username;
            this.Role = item.Role.Name;
            this.Domain = item.Domain?.DomainName;
            return this;
        }
    }
}
