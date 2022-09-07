namespace DSM.UI.Api.Models.AzureDevOps
{
    public class AzurePortalInventory
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string Bindings { get; set; }
        public string DefaultHostName { get; set; }
        public string OutboundIpAddresses { get; set; }
        public string ResourceGroup { get; set; }
        public string SubscriptionName {get; set;}
    }
}