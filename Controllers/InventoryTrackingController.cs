using System;
using System.Threading.Tasks;
using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.EditableInventory;
using DSM.UI.Api.Models.LogModels;
using DSM.UI.Api.Models.User;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DSM.UI.Api.Controllers
{
    public class InventoryTrackingController : ControllerBase
    {
        private readonly IInventoryTrackingService _inventoryTrackingService;
        private readonly IDSMOperationLogger _operationLogger;

        public InventoryTrackingController(IInventoryTrackingService inventoryTrackingService,
            IDSMOperationLogger operationLogger)
        {
            _inventoryTrackingService = inventoryTrackingService;
            _operationLogger = operationLogger;
        }

        #region UpdatedSiteInventory Region

        [HttpGet("UpdatedSiteInventory/")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetAllUpdatedSiteInventories()
        {
            var result = await _inventoryTrackingService.GetAllSiteInventory();
            if (result == null)
                return NotFound(new { message = "No records found" });

            return Ok(result);
        }

        [HttpGet("UpdatedSiteInventory/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetUpdatedSiteInventoryById(int id)
        {
            var result = await _inventoryTrackingService.GetSiteInventoryItem(id);
            if (result == null)
                return NotFound(new { message = "No records found" });

            return Ok(result);
        }

        [HttpPost("UpdatedSiteInventory/")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public async Task<IActionResult> CreateUpdatedSiteInventory(
            [FromBody] UpdatedSiteInventoryItem updatedSiteInventoryItem)
        {
            var result = await _inventoryTrackingService.AddUpdatedSiteInventoryItemAsync(updatedSiteInventoryItem);
            if (result == null)
                return BadRequest(new { message = "Error while creating record" });

            await _operationLogger.LogOperationToDbAsync(new OperationLog
            {
                LoggedOperation = "CreateUpdatedSiteInventory", LogType = "Create",
                LogLocation = "UpdatedSiteInventory",
                UserName = User.Identity.Name,
                AffectedObjectId = result.Id
            });

            return Ok(result);
        }

        [HttpPost("UpdatedSiteInventory/update")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public async Task<IActionResult> UpdateUpdatedSiteInventoryInventory(
            [FromBody] UpdatedSiteInventoryItem updatedSiteInventoryItem)
        {
            var result = await _inventoryTrackingService.UpdateUpdatedSiteInventoryItemAsync(updatedSiteInventoryItem);
            if (result == null)
                return BadRequest(new { message = "Error while updating record" });

            await _operationLogger.LogOperationToDbAsync(new OperationLog
            {
                LoggedOperation = "UpdateUpdatedSiteInventory", LogType = "Update",
                LogLocation = "UpdatedSiteInventory",
                UserName = User.Identity.Name,
                AffectedObjectId = result.Id,
            });

            return Ok(new { Message = "Record updated successfully", UpdatedEntity = result });
        }

        [HttpGet("UpdatedSiteInventory/delete/{id}")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public async Task<IActionResult> DeleteUpdatedSiteInventory(int id)
        {
            var result = await _inventoryTrackingService.DeleteUpdatedSiteInventoryItemAsync(id);
            if (!result)
                return BadRequest(new { message = "Error while deleting record" });

            await _operationLogger.LogOperationToDbAsync(new OperationLog
            {
                LoggedOperation = "DeleteUpdatedSiteInventory", LogType = "Delete",
                LogLocation = "UpdatedSiteInventory",
                UserName = User.Identity.Name,
                AffectedObjectId = id
            });

            return Ok(new { message = "Record deleted successfully" });
        }

        [HttpGet("UpdatedSiteInventory/Export/")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportUpdatedSiteInventory()
        {
            var exportData = _inventoryTrackingService.DownloadUpdatedSiteInventory();
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_UPDATED_SITE_INVENTORY_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }


        [HttpGet("UpdatedSiteInventory/Export/{term}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public IActionResult ExportUpdatedSiteInventory(string term)
        {
            var exportData = _inventoryTrackingService.DownloadUpdatedSiteInventory(term);
            if (exportData == null) return BadRequest(InvalidOperationError.GetInstance());

            string date = DateTime.Now.ToString("yyyyMMdd");

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("DSM_EXPORT_UPDATED_SITE_INVENTORY_{0}.xlsx", date),
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(exportData, "application/octet-stream");
        }

        #endregion
    }
}