using DSM.UI.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.User
{
    public class UpdateModel : IMappable<User>
    {
        public string Fullname { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public IMappable<User> Map(User item)
        {
            this.Fullname = item.FullName;
            this.Password = item.Password;
            this.RoleId = item.RoleId;
            return this;
        }
    }
}
