using System.Linq;
using System.Threading.Tasks;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DSM.UI.Api.Controllers
{
    public class CustomInventoryController : Controller
    {
        private readonly ICustomInventoryService _customInventoryService;

        public CustomInventoryController(ICustomInventoryService customInventoryService)
        {
            _customInventoryService = customInventoryService;
        }

        [HttpGet]
        [Route("NetworkInventory")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetNetworkInventory()
        {
            var result = await _customInventoryService.GetNetworkInventory();

            if (!result.Any())
                return NotFound("There is no data in the database");

            return Ok(result);
        }
        
        [HttpGet]
        [Route("NetworkSecurityInventory")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetNetworkSecurityInventory()
        {
            var result = await _customInventoryService.GetNetworkSecurityInventory();

            if (!result.Any())
                return NotFound("There is no data in the database");

            return Ok(result);
        }
        
        [HttpGet]
        [Route("FrameworkVersionInventory")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetFrameworkVersionInventory()
        {
            var result = await _customInventoryService.GetFrameworkVersionInventory();

            if (!result.Any())
                return NotFound("There is no data in the database");

            return Ok(result);
        }
    }
}