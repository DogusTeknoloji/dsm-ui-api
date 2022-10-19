namespace DSM.UI.Api.Models.EditableInventory
{
    public class DetailedServerInventoryItem
    {
        public int Id { get; set; }
        public string ServerName { get; set; }
        public string OS { get; set; }
        public string IP { get; set; }
        public string Responsible { get; set; }
        public string Server { get; set; }
        public string Application { get; set; }
    }
}