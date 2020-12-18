namespace DSM.UI.Api.Models.Dashboard
{
    public class ElasticSearchInventoryDetails
    {
        public string Description { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Hostname { get; set; }
        public string IpAddress { get; set; }
        public string LoadbalancerIp { get; set; }
        public string CompanyName { get; set; }
        public int ServerId { get; set; }
        public int CompanyId { get; set; }
    }
}