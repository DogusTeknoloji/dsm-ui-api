using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSM.UI.Api.Models.Dashboard
{
    public class AppManagementLink
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppId { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
    }
}