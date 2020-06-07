using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.Caching;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DSM.UI.Api.Models.AzureDevOps
{
    public class ProjectResult : ICachable
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUrl { get; set; }
        public string ProjectState { get; set; }
        public int ProjectRevision { get; set; }
        public bool ProjectPublicVisibility { get; set; }
        public DateTime ProjectLastUpdateTime { get; set; }
        public string CacheId { get => this.ProjectId; set { } }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ExpireDate { get; set; } = DateTime.Now.AddMinutes(15);
    }
}
