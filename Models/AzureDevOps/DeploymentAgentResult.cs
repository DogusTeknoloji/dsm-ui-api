using System;

namespace DSM.UI.Api.Models.AzureDevOps
{
    public class DeploymentAgentResult : ICachable
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string DeploymentGroupId { get; set; }
        public string DeploymentGroupName { get; set; }
        public int MaxParellelism { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime StatusChangedOn { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string AgentVersion { get; set; }
        public string OperatingSystem { get; set; }
        public string Enabled { get; set; }
        public string Status { get; set; }

        public string CacheId => this.AgentId.ToString();
    }
}
