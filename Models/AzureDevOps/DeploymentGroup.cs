using DSM.UI.Api.Helpers;

namespace DSM.UI.Api.Models.AzureDevOps
{
    public class DeploymentGroup : IConvertible<DeploymentGroupResult>
    {
        public int MachineCount { get; set; }
        public int Id { get; set; }
        public Project Project { get; set; }
        public string Name { get; set; }
        public Pool Pool { get; set; }

        public DeploymentGroupResult Convert()
        {
            DeploymentGroupResult resultSet = new DeploymentGroupResult
            {
                DeploymentGroupId = this.Id,
                DeploymentGroupName = this.Name,
                DeploymentGroupPool = this.Pool.Name,
                MachineCount = this.MachineCount,
                ProjectName = this.Project.Name,
                ProjectId = this.Project.Id
            };
            return resultSet;
        }
    }
}
