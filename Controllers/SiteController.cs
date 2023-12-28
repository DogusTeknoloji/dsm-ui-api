using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.Site;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SiteController : ControllerBase
    {
        private readonly ISiteService _siteService;

        public SiteController(ISiteService siteService, IOptions<AppSettings> appSettings)
        {
            this._siteService = siteService;
        }

        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        [HttpGet("Search/{term}")]
        public IActionResult Search(string term)
        {
            var searchResults = _siteService.SearchSites(term);
            if (searchResults == null)
                return this.BadRequest(new { message = "Invalid search term" });

            return Ok(searchResults);
        }

        [HttpGet("header/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsHeader(int id)

        {
            var headerInfo = _siteService.GetHeader(id);
            if (headerInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(headerInfo);
        }
        [HttpGet("details/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsGeneral(int id)
        {
            var generalInfo = _siteService.GetDetailsGeneral(id);
            if (generalInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(generalInfo);
        }

        [HttpGet("bindings/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsBindings(int id)
        {
            var bindingInfo = _siteService.GetDetailsBindings(id);
            if (bindingInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(bindingInfo);
        }

        [HttpGet("packages/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsPackage(int id)
        {
            var packageInfo = _siteService.GetDetailsPackages(id);
            if (packageInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(packageInfo);
        }

        [HttpGet("endpoints/{id}")]
        [Authorize(Roles = "Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsEndpoints(int id)
        {
            var endpointInfo = _siteService.GetDetailsEndpoint(id);
            if (endpointInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(endpointInfo);
        }

        [HttpGet("connectionstrings/{id}")]
        [Authorize(Roles = "Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsConnectionString(int id)
        {
            var connectionstringInfo = _siteService.GetDetailsConnectionStrings(id);
            if (connectionstringInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(connectionstringInfo);
        }
        [HttpGet("{pageNumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetSites(int pageNumber, [FromQuery(Name = "fi")]string fieldName, [FromQuery(Name = "pos")]int orderPosition)
        {
            var siteInfo = _siteService.GetSites(pageNumber, fieldName, orderPosition);
            if (siteInfo == null) return BadRequest(InvalidOperationError.GetInstance());

            return Ok(siteInfo);
        }
        [HttpGet("letters")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetLetters()
        {
            var letters = this._siteService.GetLetters();
            if (letters == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(letters);
        }

        [HttpGet("letters/{letter}&{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetSitesByLetter(string letter, int pagenumber)
        {
            var siteInfo = this._siteService.GetSitesByLetter(letter, pagenumber);
            if (siteInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(siteInfo);
        }

        [HttpGet("export/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportSites(string term)
        {
            var exportData = this._siteService.DownloadSites(term);
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_SITES_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }
        [HttpGet("export")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportSites()
        {
            var exportData = this._siteService.DownloadSites();
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_SITES_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }
    }
}
