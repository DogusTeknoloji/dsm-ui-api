using DSM.UI.Api.Helpers;
using DSM.UI.Api.Helpers.RemoteDesktop.Models;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerController : ControllerBase
    {
        private readonly IServerService _serverService;
        public ServerController(IServerService serverService, IOptions<AppSettings> appSettings)
        {
            this._serverService = serverService;
        }
        [HttpGet("Search/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult Search(string term)
        {
            var searchResults = this._serverService.SearchServers(term);
            if (searchResults == null)
                return this.BadRequest(new { message = "Invalid search term" });

            return Ok(searchResults);
        }
        [HttpGet("header/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsHeader(int id)
        {
            var headerInfo = this._serverService.GetHeader(id);
            if (headerInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(headerInfo);
        }
        [HttpGet("details/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetDetailsGeneral(int id)
        {
            var generalInfo = this._serverService.GetDetailsGeneral(id);
            if (generalInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(generalInfo);
        }
        [HttpGet("{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetServers(int pagenumber, [FromQuery(Name = "fi")]string fieldName, [FromQuery(Name = "pos")]int orderPosition)
        {
            var serverInfo = this._serverService.GetServers(pagenumber, fieldName, orderPosition);
            if (serverInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(serverInfo);
        }
        [HttpGet("letters")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetLetters()
        {
            var letters = this._serverService.GetLetters();
            if (letters == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(letters);
        }
        [HttpGet("letters/{letter}&{pagenumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetServersByLetter(string letter, int pagenumber)
        {
            var serverInfo = this._serverService.GetServersByLetter(letter, pagenumber);
            if (serverInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(serverInfo);
        }
        [HttpGet("sites/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetSites(int id)
        {
            var siteInfo = this._serverService.GetDetailsSites(id);
            if (siteInfo == null) return BadRequest(InvalidOperationError.GetInstance());
            return Ok(siteInfo);
        }

        [HttpGet("export/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportServers(string term)
        {
            var exportData = this._serverService.DownloadServers(term);
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_SERVERS_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }

        [HttpGet("export/")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportServers()
        {
            var exportData = this._serverService.DownloadServers();
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_SERVERS_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }

        [HttpPost("connect/")]
        [Authorize(Roles = "Manager, Administrator, CIFANG")]
        public IActionResult ConnectServer([FromBody] RdpInfo rdpInfo)
        {
            var rdpFile = this._serverService.DownloadRDPFile(rdpInfo);
            if (rdpFile == null) return BadRequest(InvalidOperationError.GetInstance());

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "connection.rdp",
                Inline = false
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(rdpFile, "application/octet-stream");
        }

        [HttpGet("ServerCheckDate/")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult GetServerCheckDate()
        {
            var checkDate = this._serverService.GetServerCheckDate();
            if (checkDate == null) return NotFound();
            return Ok(checkDate);
        }
    }
}