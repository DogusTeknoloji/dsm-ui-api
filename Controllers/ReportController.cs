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
    public class ReportController : ControllerBase
    {
        private readonly IReportsService _reportsService;

        public ReportController(IReportsService reportsService, IOptions<AppSettings> appSettings)
        {
            this._reportsService = reportsService;
        }

        [HttpGet("overalldiskstatus/{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetOverallDiskStatus(int pagenumber)
        {
            var reportInfo = this._reportsService.GetOverallDiskStatus(pagenumber);
            if (reportInfo == null) return this.BadRequest(InvalidOperationError.GetInstance());
            return this.Ok(reportInfo);
        }

        [HttpGet("scheduledjobstatus/{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetScheduledJobStatus(int pagenumber)
        {
            var reportInfo = this._reportsService.GetScheduledJobs(pagenumber);
            if (reportInfo == null) return this.BadRequest(InvalidOperationError.GetInstance());
            return this.Ok(reportInfo);
        }

        [HttpGet("overalldiskstatus/search/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult SearchOverallDiskStatus(string term)
        {
            var searchResults = this._reportsService.SearchOverallDiskStatus(term);
            if (searchResults == null) return this.BadRequest(new { message = "Invalid search term" });
            return Ok(searchResults);
        }

        [HttpGet("scheduledjobstatus/search/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult SearchScheduledJobStatus(string term)
        {
            var searchResults = this._reportsService.SearchScheduledJobList(term);
            if (searchResults == null) return this.BadRequest(new { message = "Invalid search term" });
            return Ok(searchResults);
        }

        [HttpGet("overalldiskstatus/export/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportOverallDiskStatus(string term)
        {
            var exportData = this._reportsService.DownloadOverallDiskStatus(term);
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_OVERALLDISKSTATUS_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }

        [HttpGet("overalldiskstatus/export/")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportOverallDiskStatus()
        {
            var exportData = this._reportsService.DownloadOverallDiskStatus();
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_OVERALLDISKSTATUS_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }

        [HttpGet("scheduledjobstatus/export/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportScheduledJobStatus(string term)
        {
            var exportData = this._reportsService.DownloadScheduledJobList(term);
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_SCHEDULEDJOBSTATUS_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }

        [HttpGet("scheduledjobstatus/export/")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportScheduledJobStatus()
        {
            var exportData = this._reportsService.DownloadScheduledJobList();
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_SCHEDULEDJOBSTATUS_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }

        [HttpGet("odmstatusreport/{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetODMStatusReport(int pagenumber)
        {
            var ODMStatusList = this._reportsService.GetODMItems(pagenumber);
            if (ODMStatusList == null) return BadRequest(InvalidOperationError.GetInstance());

            return this.Ok(ODMStatusList); 
        }

        [HttpGet("odmstatusreport/search/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult SearchODMStatusReport(string term)
        {
            var searchResults = this._reportsService.GetSearchODMItems(term);
            if (searchResults == null)
                return this.BadRequest(InvalidOperationError.GetInstance());

            return Ok(searchResults);
        }

        [HttpGet("odmstatusreport/export/")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportODMStatusReport()
        {
            var exportData = this._reportsService.DownloadODMItems();
            if (exportData == null) return this.BadRequest(InvalidOperationError.GetInstance());

            return File(exportData, "application/octet-stream");
        }

        [HttpGet("odmstatusreport/export/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportODMStatusReport(string term)
        {
            var exportData = this._reportsService.DownloadODMItems(term);
            if (exportData == null) return this.BadRequest(InvalidOperationError.GetInstance());

            return File(exportData, "application/octet-stream");
        }
    }
}