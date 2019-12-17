using System.Collections.Generic;

namespace DSM.UI.Api.Models.User
{
    public class Domain
    {
        public int DomainId { get; set; }
        public string DomainName { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
