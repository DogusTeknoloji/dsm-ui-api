using DSM.UI.Api.Helpers;

namespace DSM.UI.Api.Models.AzureDevOps
{
    public class DeploymentTarget : IConvertible<DeploymentAgentResult>
    {
        public string[] Tags { get; set; }
        public int Id { get; set; }
        public DeploymentAgent Agent { get; set; }

        public DeploymentAgentResult Convert()
        {
            if (this.Agent == null)
            {
                return null;
            }

            DeploymentAgentResult resultSet = new DeploymentAgentResult
            {
                AgentId = this.Agent.Id,
                AgentName = this.Agent.Name,
                AgentVersion = this.Agent.Version,
                CreatedOn = this.Agent.CreatedOn,
                Enabled = this.Agent.Enabled ? "Yes" : "No",
                MaxParellelism = this.Agent.MaxParellelism,
                OperatingSystem = this.Agent.OsDescription,
                Status = this.Agent.Status,
                StatusChangedOn = this.Agent.StatusChangedOn
            };
            return resultSet;
        }
    }
}