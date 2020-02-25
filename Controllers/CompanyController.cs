using DSM.UI.Api.Helpers;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private ICompanyService _companyService;
        private readonly AppSettings _appSettings;

        public CompanyController(ICompanyService companyService, IOptions<AppSettings> appSettings)
        {
            this._companyService = companyService;
            this._appSettings = appSettings.Value;
        }
        [HttpGet("Search/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult Search(string term)
        {
            var searchResults = _companyService.SearchCompanies(term);
            if (searchResults == null)
                return this.BadRequest(new { message = "Invalid search term" });
            return Ok(searchResults);
        }
        [HttpGet("header/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsHeader(int id)
        {
            var headerInfo = this._companyService.GetHeader(id);
            if (headerInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(headerInfo);
        }
        [HttpGet("servers/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsServer(int id)
        {
            var serverInfo = this._companyService.GetDetailsServers(id);
            if (serverInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(serverInfo);
        }
        [HttpGet("sites/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsSites(int id)
        {
            var siteInfo = this._companyService.GetDetailsSites(id);
            if (siteInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(siteInfo);
        }
        [HttpGet("letters")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetLetters()
        {
            var letters = this._companyService.GetLetters();
            if (letters == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(letters);
        }
        [HttpGet("letters/{letter}&{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetCompaniesByLetter(string letter, int pagenumber)
        {
            var companyInfo = this._companyService.GetCompanyByLetter(letter, pagenumber);
            if (companyInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(companyInfo);
        }
        [HttpGet("{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetCompanies(int pagenumber)
        {
            var companyInfo = this._companyService.GetCompanies(pagenumber);
            if (companyInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(companyInfo.OrderBy(x => x.Name));
        }

        [HttpGet("export/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportCompanies(string term)
        {
            var exportData = this._companyService.DownloadCompanies(term);
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_COMPANIES_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }
        [HttpGet("export/")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportCompanies()
        {
            var exportData = this._companyService.DownloadCompanies();
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_COMPANIES_FILTERED_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }
    }
}