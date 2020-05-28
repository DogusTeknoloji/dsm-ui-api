using System;

namespace DSM.UI.Api.Models.AzureDevOps
{
    public class DeploymentGroupResult : ICachable
    {
        public int DeploymentGroupId { get; set; }
        public int MachineCount { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string DeploymentGroupName { get; set; }
        public string DeploymentGroupPool { get; set; }

        public string CacheId => this.DeploymentGroupId.ToString();
    }
}
