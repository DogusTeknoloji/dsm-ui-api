using DSM.UI.Api.Helpers;
using System;

namespace DSM.UI.Api.Models.AzureDevOps
{
    public class DeploymentAgent : IConvertible<DeploymentAgentResult>
    {
        public int MaxParellelism { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime StatusChangedOn { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string OsDescription { get; set; }
        public bool Enabled { get; set; }
        public string Status { get; set; }
        public string ProvisioningState { get; set; }
        public string AccessPoint { get; set; }

        public DeploymentAgentResult Convert()
        {
            DeploymentAgentResult resultSet = new DeploymentAgentResult
            {
                AgentId = this.Id,
                AgentName = this.Name,
                AgentVersion = this.Version,
                CreatedOn = this.CreatedOn,
                Enabled = this.Enabled ? "Yes" : "No",
                MaxParellelism = this.MaxParellelism,
                OperatingSystem = this.OsDescription,
                Status = this.Status,
                StatusChangedOn = this.StatusChangedOn
            };
            return resultSet;
        }
    }
}
