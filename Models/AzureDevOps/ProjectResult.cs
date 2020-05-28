using DSM.UI.Api.Helpers;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DSM.UI.Api.Models.AzureDevOps
{
    public class ProjectResult : IConvertible<Project>, ICachable
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUrl { get; set; }
        public string ProjectState { get; set; }
        public int ProjectRevision { get; set; }
        public bool ProjectPublicVisibility { get; set; }
        public DateTime ProjectLastUpdateTime { get; set; }
        public string CacheId => this.ProjectId.ToString();

        public Project Convert()
        {
            Project p = new Project
            {
                Id = this.ProjectId,
                LastUpdateTime = this.ProjectLastUpdateTime,
                Name = this.ProjectName,
                Revision = this.ProjectRevision,
                State = this.ProjectState,
                Url = this.ProjectUrl,
                Visibility = this.ProjectPublicVisibility ? "public" : "private"
            };
            return p;
        }
    }
}
