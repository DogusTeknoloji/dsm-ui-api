using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSM.UI.Api.Models.Dashboard
{
    public class ElasticSearchInventoryItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppId { get; set; }
        public string Description { get; set; }
        [Required]
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public string Hostname { get; set; }
        public string IpAddress { get; set; }
        public string LoadbalancerIp { get; set; }
        public int ServerId { get; set; }
        [Required]
        public int CompanyId { get; set; }

        public virtual Company.Company Company { get; set; }
    }
}