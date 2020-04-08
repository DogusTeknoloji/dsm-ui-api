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
        private IDashboardService _dashboardService;
        private readonly AppSettings _appSettings;

        public DashboardController(IDashboardService dashboardService, IOptions<AppSettings> appSettings)
        {
            this._dashboardService = dashboardService;
            this._appSettings = appSettings.Value;
        }

        [HttpGet("appmanagement/")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public IActionResult GetLinks()
        {
            var links = this._dashboardService.GetLinks();
            if (links == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(links.OrderBy(x => x.Description));
        }

        [HttpGet("dashboard")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDashboard()
        {
            var result = this._dashboardService.GetDashboard();
            return Ok(result);
        }
    }
}
