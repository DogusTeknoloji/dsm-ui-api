using System;
using DSM.UI.Api.Helpers;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResponsibleController : ControllerBase
    {

        private  readonly IResponsibleService _responsibleService;

        public ResponsibleController(IResponsibleService responsibleService)
        {
            _responsibleService = responsibleService;
        }
        
        [HttpGet("responsibles")]
        [AllowAnonymous]
        public IActionResult GetResponsibles()
        {
            var responsibles = _responsibleService.GetResponsibles();
            
            if (responsibles == null)
                return this.BadRequest(new { message = "Invalid search term" });
            
            return Ok(responsibles);
        }
        
        [HttpGet("search/{term}")]
        [AllowAnonymous]
        public IActionResult Search(string term)
        {
            var searchResults = _responsibleService.SearchResponsibles(term);
            if (searchResults == null)
                return this.BadRequest(new { message = "Invalid search term" });
            
            return Ok(searchResults);
        }
        
        
        [HttpGet("servers/{responsibleName}")]
        [AllowAnonymous]
        public IActionResult GetDetailsServers(string responsibleName)
        {
            var servers = _responsibleService.GetDetailsServers(responsibleName);
            
            if (servers == null)
                return this.BadRequest(new { message = "Invalid search term" });
            
            return Ok(servers);
        }
        
        [HttpGet("sites/{responsibleName}")]
        [AllowAnonymous]
        public IActionResult GetDetailsSites(string responsibleName)
        {
            var sites = _responsibleService.GetDetailsSites(responsibleName);
            
            if (sites == null)
                return this.BadRequest(new { message = "Invalid search term" });
            
            return Ok(sites);
        }
        
        [HttpGet("export/{term}")]
        [AllowAnonymous]
        public IActionResult ExportResponsibles(string term)
        {
            var exportData = _responsibleService.DownloadResponsibles(term);
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_RESPONSIBLES_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }
        
        [HttpGet("export")]
        [AllowAnonymous]
        public IActionResult ExportCompanies()
        {
            var exportData = this._responsibleService.DownloadResponsibles();
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_RESPONSIBLES_FILTERED_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }
    }
}