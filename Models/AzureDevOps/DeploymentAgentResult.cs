using DSM.UI.Api.Models.Caching;
using MongoDB.Bson.Serialization.Attributes;
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
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string BaseId { get => this.DeploymentGroupId + this.AgentId.ToString(); set { } }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string AgentVersion { get; set; }
        public string OperatingSystem { get; set; }
        public string Enabled { get; set; }
        public string Status { get; set; }
        public string CacheId { get => this.DeploymentGroupId + this.AgentId.ToString(); set { } }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ExpireDate { get; set; } = DateTime.Now.AddMinutes(15);

    }
}
