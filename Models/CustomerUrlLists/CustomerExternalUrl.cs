namespace DSM.UI.Api.Models.CustomerUrlLists
{
    public class CustomerExternalUrl
    {
        public int Id { get; set; }
        public string ApplicationTeam { get; set; }
        public string FromServer { get; set; }
        public string SourceIpPort { get; set; }
        public string LoadBalancerIP { get; set; }
        public string FrontApp { get; set; }
        public string DestinationURL { get; set; }
        public string DestinationIP_Port { get; set; }
        public string ServiceExplanation { get; set; }
        public string IsFollowing { get; set; }
    }
}