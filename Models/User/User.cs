using DSM.UI.Api.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSM.UI.Api.Models.User
{
    public class User : IMappable<UpdateModel>, IMappable<RegisterModel>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string DomainService { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastAttempt { get; set; }
        public string FullName { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string AuthKey { get; set; }
        public int? DomainId { get; set; }
        public virtual Domain Domain { get; set; }
        public string ProfileImage { get; set; }


        public IMappable<UpdateModel> Map(UpdateModel item)
        {
            this.Password = item.Password;
            this.FullName = item.Fullname;
            this.RoleId = item.RoleId;
            return this;
        }

        public IMappable<RegisterModel> Map(RegisterModel item)
        {
            this.FullName = item.FullName;
            this.Username = item.Username;
            this.Password = item.Password;
            this.DomainId = item.DomainId;
            return this;
        }
    }
}
