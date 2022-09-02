using DSM.UI.Api.Helpers;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService, IOptions<AppSettings> appSettings)
        {
            this._dashboardService = dashboardService;
        }

        [HttpGet("appmanagement/")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public IActionResult GetLinks()
        {
            var links = this._dashboardService.GetLinks();
            if (links == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(links.OrderBy(x => x.Description));
        }

        [HttpGet("elastic-search-inventory/")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public IActionResult GetElasticSearchInventory()
        {
            var inventoryItems = this._dashboardService.GetElasticSearchInventory();
            if (inventoryItems == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(inventoryItems.OrderBy(x => x.Description));
        }

        [HttpGet("dashboard")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDashboard()
        {
            var result = this._dashboardService.GetDashboard();
            return Ok(result);
        }

        [HttpGet("allCounts")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetAllCounts()
        {
            var result = this._dashboardService.GetAllCounts();
            return Ok(result);
        }

        [HttpGet("serverCount")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetServerCounts()
        {
            var result = this._dashboardService.GetTotalServerCount();
            return Ok(new { serverCount = result });
        }

        [HttpGet("siteCount")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetSiteCounts()
        {
            var result = this._dashboardService.GetTotalSiteCount();
            return Ok(new { siteCount = result });
        }

        [HttpGet("responsibilityCount")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetResponsibilityCounts()
        {
            var result = this._dashboardService.GetTotalResponsibilityCount();
            return Ok(new { responsibilityCount = result });
        }

        [HttpGet("dbCount")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDbCounts()
        {
            var result = this._dashboardService.GetTotalDbCount();
            return Ok(new { dbCount = result });
        }

        [HttpGet("userCount")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetUserCounts()
        {
            var result = this._dashboardService.GetTotalUserCount();
            return Ok(new { userCount = result });
        }
    }
}