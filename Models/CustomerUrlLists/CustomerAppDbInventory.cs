namespace DSM.UI.Api.Models.CustomerUrlLists
{
    public class CustomerAppDbInventory
    {
        public int Id { get; set; }
        public string Server { get; set; }
        public string AppName { get; set; }
        public string DBIP { get; set; }
        public string DbName { get; set; }
        public string DBPort { get; set; }
    }
}