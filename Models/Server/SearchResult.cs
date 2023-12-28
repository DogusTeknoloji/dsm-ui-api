namespace DSM.UI.Api.Models.Server
{
    public class SearchResult
    {
        public int ServerId { get; set; }
        public string MachineName { get; set; }
        public string OperatingSystem { get; set; }
        public string CompanyName { get; set; }
        public string Responsible { get; set; }
        public string DnsName { get; set; }
        public string IpAddress { get; set; }
        public string ServiceName { get; set; }
        public string? Notes { get; set; }
    }
}
