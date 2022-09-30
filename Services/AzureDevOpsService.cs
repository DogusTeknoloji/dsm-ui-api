using DSM.UI.Api.Helpers.AzureDevOps;
using DSM.UI.Api.Models.AzureDevOps;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;

namespace DSM.UI.Api.Services
{
    public interface IAzureDevOpsService
    {
        Task<IEnumerable<ProjectResult>> GetProjectsAsync();
        Task<IEnumerable<DeploymentGroupResult>> GetDeploymentGroupsAsync();
        Task<IEnumerable<DeploymentAgentResult>> GetDeploymentAgentsAsync();
        Task<IEnumerable<AzurePortalInventory>> GetAzurePortalInventoryAsync();
        Task<AzurePortalSiteNameWithBindings> GetAzurePortalInventoryItem(int id);
        Task<IEnumerable<AzurePortalSiteNameWithBindings>> GetSiteNamesWithBindings();
    }

    public partial class AzureDevOpsService : IAzureDevOpsService
    {
        private readonly string _organization = RequestHelper.AzureDevOpsOrganizationName;
        private readonly DSMStorageDataContext _context;

        public AzureDevOpsService(DSMStorageDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectResult>> GetProjectsAsync()
        {
            CacheDBService<ProjectResult> cacheService = new CacheDBService<ProjectResult>("AzureDevOpsProjects");
            IEnumerable<ProjectResult> cacheResults = cacheService.Get();
            if (cacheResults.Count() < 1)
            {
                string projectsUrl = AzureDevOpsUrl.ProjectsUrl(_organization);

                string rawJson = await RequestHelper.Evaluate(projectsUrl);

                GenericHolder<Project> projectHolder = JsonConvert.DeserializeObject<GenericHolder<Project>>(rawJson);

                if (projectHolder != null && projectHolder?.Count > 0)
                {
                    IEnumerable<Project> projects = projectHolder.Value;
                    IEnumerable<ProjectResult> results = projects.Select(p => p.Convert());
                    cacheService.CreateMultiple(results, overwrite: true);

                    return results;
                }
            }

            return cacheResults;
        }

        public async Task<IEnumerable<DeploymentGroupResult>> GetDeploymentGroupsAsync()
        {
            CacheDBService<DeploymentGroupResult> cacheService =
                new CacheDBService<DeploymentGroupResult>("AzureDevOpsDeploymentGroups");
            IEnumerable<DeploymentGroupResult> cacheResults = cacheService.Get();
            if (cacheResults.Count() < 1)
            {
                List<DeploymentGroupResult> results = new List<DeploymentGroupResult>();
                IEnumerable<ProjectResult> projects = await this.GetProjectsAsync();
                foreach (ProjectResult project in projects)
                {
                    string projectId = project.ProjectId.ToString();
                    string deploymentGroupsUrl = AzureDevOpsUrl.DeploymentGroupsUrl(_organization, projectId);

                    string rawJson = await RequestHelper.Evaluate(deploymentGroupsUrl);

                    GenericHolder<DeploymentGroup> deploymentGroupHolder =
                        JsonConvert.DeserializeObject<GenericHolder<DeploymentGroup>>(rawJson);

                    if (deploymentGroupHolder != null && deploymentGroupHolder?.Count > 0)
                    {
                        IEnumerable<DeploymentGroup> deploymentGroups = deploymentGroupHolder.Value;
                        var subResults = deploymentGroups.Select(p => p.Convert());
                        results.AddRange(subResults);
                    }
                }

                cacheService.CreateMultiple(results, overwrite: true);
                return results;
            }

            return cacheResults;
        }

        public async Task<IEnumerable<DeploymentAgentResult>> GetDeploymentAgentsAsync()
        {
            CacheDBService<DeploymentAgentResult> cacheService =
                new CacheDBService<DeploymentAgentResult>("AzureDevOpsAgents");
            IEnumerable<DeploymentAgentResult> cacheResults = cacheService.Get();
            if (cacheResults.Count() < 1)
            {
                List<DeploymentAgentResult> results = new List<DeploymentAgentResult>();
                IEnumerable<DeploymentGroupResult> deploymentGroups = await this.GetDeploymentGroupsAsync();
                foreach (DeploymentGroupResult deploymentGroup in deploymentGroups)
                {
                    string projectId = deploymentGroup.ProjectId.ToString();
                    string deploymentGroupId = deploymentGroup.DeploymentGroupId.ToString();

                    string deploymentTargetsUrl =
                        AzureDevOpsUrl.TargetsUrl(_organization, projectId, deploymentGroupId);
                    string rawJson = await RequestHelper.Evaluate(deploymentTargetsUrl);

                    GenericHolder<DeploymentTarget> deploymentTargetHolder =
                        JsonConvert.DeserializeObject<GenericHolder<DeploymentTarget>>(rawJson);

                    if (deploymentTargetHolder != null && deploymentTargetHolder?.Count > 0)
                    {
                        IEnumerable<DeploymentTarget> deploymentTargets = deploymentTargetHolder.Value;
                        foreach (DeploymentTarget deploymentTarget in deploymentTargets)
                        {
                            if (deploymentTarget.Agent == null) continue;
                            var subResult = deploymentTarget.Convert();

                            subResult.DeploymentGroupId = deploymentGroup.DeploymentGroupId.ToString();
                            subResult.DeploymentGroupName = deploymentGroup.DeploymentGroupName;
                            subResult.ProjectId = deploymentGroup.ProjectId.ToString();
                            subResult.ProjectName = deploymentGroup.ProjectName;

                            results.Add(subResult);
                        }
                    }
                }

                cacheService.CreateMultiple(results, overwrite: true);
                return results;
            }

            return cacheResults;
        }

        public async Task<IEnumerable<AzurePortalInventory>> GetAzurePortalInventoryAsync()
        {
            return await _context.AzurePortalInventory.ToListAsync();
        }

        public async Task<AzurePortalSiteNameWithBindings> GetAzurePortalInventoryItem(int id)
        {
            var portalItem = await _context.AzurePortalInventory.FirstOrDefaultAsync(i => i.Id == id);
            var apInventory = await _context.AzurePortalInventory.Where(s => s.SiteName == portalItem.SiteName).ToListAsync();

            var result = new AzurePortalSiteNameWithBindings
            {
                SiteName = portalItem.SiteName,
                ResourceGroup = portalItem.ResourceGroup,
                Id = portalItem.Id,
                Bindings = new List<string>(),
                DefaultHostName = portalItem.DefaultHostName,
                OutboundIpAddresses = portalItem.OutboundIpAddresses,
                SubscriptionName = portalItem.SubscriptionName
            };
            
            result.Bindings.AddRange(apInventory.Select(x => x.Bindings));

            return result;
        }

        public async Task<IEnumerable<AzurePortalSiteNameWithBindings>> GetSiteNamesWithBindings()
        {
            var resultList = new List<AzurePortalSiteNameWithBindings>();
            var azurePortal = await _context.AzurePortalInventory.ToListAsync();

            foreach (var item in azurePortal)
            {
                var siteName = item.SiteName;
                var binding = item.Bindings;

                var siteNameWithBindings = resultList.FirstOrDefault(x => x.SiteName == siteName);

                if (siteNameWithBindings == null)
                    resultList.Add(new AzurePortalSiteNameWithBindings
                    {
                        SiteName = siteName,
                        Bindings = new List<string> { binding },
                        Id = item.Id,
                        ResourceGroup = item.ResourceGroup,
                        DefaultHostName = item.DefaultHostName,
                        OutboundIpAddresses = item.OutboundIpAddresses,
                        SubscriptionName = item.SubscriptionName
                    });
                else
                    siteNameWithBindings.Bindings.Add(binding);
            }

            return resultList;
        }
    }
}