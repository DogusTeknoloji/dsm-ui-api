using DSM.UI.Api.Helpers;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonitoringController : ControllerBase
    {
        private readonly IMonitoringService _monitoringService;

        public MonitoringController(IMonitoringService MonitoringService, IOptions<AppSettings> appSettings)
        {
            this._monitoringService = (IMonitoringService)MonitoringService;
        }

        [HttpGet("alerts/{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult Alerts(int pagenumber)
        {
            var AlertsList = this._monitoringService.GetAlertsItems(pagenumber);
            if (AlertsList == null) return BadRequest(InvalidOperationError.GetInstance());

            return this.Ok(AlertsList);
        }

    }
}
