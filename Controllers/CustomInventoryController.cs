using System;
using System.Linq;
using System.Threading.Tasks;
using DSM.UI.Api.Helpers;
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
        
        [HttpGet]
        [Route("EMBindingInventory/{pageNumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetEmBindingInventory(int pageNumber)
        {
            var result = await _customInventoryService.GetEmBindingInventory(pageNumber);

            if (!result.Any())
                return NotFound("There is no data in the database");

            return Ok(result);
        }
        
        [HttpGet]
        [Route("EMBindingInventoryByTerm/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetEmBindingInventoryByTerm(string term)
        {
            var result = await _customInventoryService.GetEmBindingInventoryByTerm(term);

            if (!result.Any())
                return NotFound("There is no data in the database");

            return Ok(result);
        }
        
        [HttpGet("EMBindingInventoryExportByTerm/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult EmBindingInventoryExportByTerm(string term)
        {
            var exportData = _customInventoryService.DownloadEmBindingInventoryByTerm(term);
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_EM_BINDINGS_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }

        [HttpGet("EMBindingInventoryExport/")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult EmBindingInventoryExport()
        {
            var exportData = _customInventoryService.DownloadEmBindingInventory();
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_EM_BINDINGS_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }
    }
}