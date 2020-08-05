using DSM.UI.Api.Models.Caching;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DSM.UI.Api.Models.AzureDevOps
{
    public class DeploymentGroupResult : ICachable
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public int DeploymentGroupId { get; set; }
        public int MachineCount { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string DeploymentGroupName { get; set; }
        public string DeploymentGroupPool { get; set; }
        public string CacheId { get => this.DeploymentGroupId.ToString(); set { } }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ExpireDate { get; set; } = DateTime.Now.AddMinutes(15);
    }
}
