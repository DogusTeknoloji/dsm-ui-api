using DSM.UI.Api.Helpers;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AzureDevOpsController : ControllerBase
    {
        private readonly IAzureDevOpsService _azureDevOpsService;

        public AzureDevOpsController(IAzureDevOpsService azureDevOpsService)
        {
            this._azureDevOpsService = azureDevOpsService;
        }

        [HttpGet("projects")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public async Task<IActionResult> GetProjectsAsync()
        {
            var projectInfo = await this._azureDevOpsService.GetProjectsAsync();
            if (projectInfo == null) return this.BadRequest(InvalidOperationError.GetInstance());
            return this.Ok(projectInfo);
        }

        [HttpGet("deployment-groups")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public async Task<IActionResult> GetDeploymentGroupsAsync()
        {
            var deploymentGroupInfo = await this._azureDevOpsService.GetDeploymentGroupsAsync();
            if (deploymentGroupInfo == null) return this.BadRequest(InvalidOperationError.GetInstance());
            return this.Ok(deploymentGroupInfo);
        }

        [HttpGet("deployment-agents")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public async Task<IActionResult> GetDeploymentAgents()
        {
            var deploymentAgentInfo = await this._azureDevOpsService.GetDeploymentAgentsAsync();
            if (deploymentAgentInfo == null) return this.BadRequest(InvalidOperationError.GetInstance());
            return this.Ok(deploymentAgentInfo);
        }

        [HttpGet("azure-portal-inventory")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async  Task<IActionResult> GetAzurePortalInventory()
        {
            var azurePortalInventory = await this._azureDevOpsService.GetAzurePortalInventoryAsync();
            if (azurePortalInventory == null) return this.BadRequest(InvalidOperationError.GetInstance());
            return this.Ok(azurePortalInventory);
        }

        [HttpGet("azure-portal-inventory/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetAzurePortalInventoryById(int id)
        {
            var result = await this._azureDevOpsService.GetAzurePortalInventoryItem(id);
            
            if (result == null) return this.BadRequest(InvalidOperationError.GetInstance());
            
            return this.Ok(result);
        }
        
        [HttpGet("azure-portal-inventory/site-bindings")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetAzurePortalInventoryBySiteName()
        {
            var result = await this._azureDevOpsService.GetSiteNamesWithBindings();
            
            if (result == null) return this.BadRequest(InvalidOperationError.GetInstance());
            
            return this.Ok(result);
        }
    }
}
