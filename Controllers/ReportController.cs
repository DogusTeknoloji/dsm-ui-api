using DSM.UI.Api.Helpers;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private IReportsService _reportsService;
        private readonly AppSettings _appSettings;

        public ReportController(IReportsService reportsService, IOptions<AppSettings> appSettings)
        {
            this._reportsService = reportsService;
            this._appSettings = appSettings.Value;
        }

        [HttpGet("overalldiskstatus/{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetOverallDiskStatus(int pagenumber)
        {
            var reportInfo = this._reportsService.GetOverallDiskStatus(pagenumber);
            if (reportInfo == null) return this.BadRequest(InvalidOperationError.GetInstance());
            return this.Ok(reportInfo);
        }
    }
}
