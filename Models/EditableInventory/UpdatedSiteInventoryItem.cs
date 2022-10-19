namespace DSM.UI.Api.Models.EditableInventory
{
    public class UpdatedSiteInventoryItem
    {
        public int Id { get; set; }
        public string MachineName { get; set; }
        public string SiteName { get; set; }
        public string Company { get; set; }
        public string Responsible { get; set; }
        public string? Owner { get; set; }
    }
}