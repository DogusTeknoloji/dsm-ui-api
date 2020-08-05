namespace DSM.UI.Api.Services
{
    public partial class AzureDevOpsService
    {
        public static class AzureDevOpsUrl
        {
            public static string ProjectsUrl(string organization) => $"https://dev.azure.com/{organization}/_apis/projects?api-version=5.1";
            public static string DeploymentGroupsUrl(string organization, string projectId) => $"https://dev.azure.com/{organization}/{projectId}/_apis/distributedtask/deploymentgroups?api-version=5.1-preview.1";
            public static string TargetsUrl(string organization, string projectId, string deploymentGroupId) => $"https://dev.azure.com/{organization}/{projectId}/_apis/distributedtask/deploymentgroups/{deploymentGroupId}/targets?api-version=5.1-preview.1";
        }
    }
}
