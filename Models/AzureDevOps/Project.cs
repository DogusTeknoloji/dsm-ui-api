using DSM.UI.Api.Helpers;
using System;

namespace DSM.UI.Api.Models.AzureDevOps
{
    public class Project : IConvertible<ProjectResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string State { get; set; }
        public int Revision { get; set; }
        public string Visibility { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public ProjectResult Convert()
        {
            ProjectResult p = new ProjectResult
            {
                ProjectId = this.Id.ToString(),
                ProjectLastUpdateTime = this.LastUpdateTime,
                ProjectName = this.Name,
                ProjectPublicVisibility = this.Visibility != "private",
                ProjectRevision = this.Revision,
                ProjectState = this.State,
                ProjectUrl = this.Url
            };
            return p;
        }
    }
}
