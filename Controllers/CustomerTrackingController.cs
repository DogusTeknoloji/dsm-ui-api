using System;
using System.Threading.Tasks;
using DSM.UI.Api.Helpers;
using DSM.UI.Api.Models.CustomerUrlLists;
using DSM.UI.Api.Models.LogModels;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace DSM.UI.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrator, CIFANG")]
    [Route("[controller]")]
    public class CustomerTrackingController : ControllerBase
    {
        private readonly ICustomerTrackingService _customerTrackingService;
        private readonly IDSMOperationLogger _operationLogger;

        public CustomerTrackingController(ICustomerTrackingService customerTrackingService,
            IDSMOperationLogger operationLogger)
        {
            _customerTrackingService = customerTrackingService;
            _operationLogger = operationLogger;
        }

        #region CustomerAppDbInventory Region

        [HttpGet("AppDb/")]
        public async Task<IActionResult> GetAllCustomerAppDbInventories()
        {
            var result = await _customerTrackingService.GetAllCustomerAppDbInventoriesAsync();
            if (result == null)
                return NotFound(new { message = "No records found" });

            return Ok(result);
        }

        [HttpGet("AppDb/{id}")]
        public async Task<IActionResult> GetCustomerAppDbInventoryById(int id)
        {
            var result = await _customerTrackingService.GetCustomerAppDbInventoryAsync(id);
            if (result == null)
                return NotFound(new { message = "No records found" });

            return Ok(result);
        }

        [HttpPost("AppDb/")]
        public async Task<IActionResult> CreateCustomerAppDbInventory(
            [FromBody] CustomerAppDbInventory customerAppDbInventory)
        {
            var result = await _customerTrackingService.AddCustomerAppDbInventoryAsync(customerAppDbInventory);
            if (result == null)
                return BadRequest(new { message = "Error while creating record" });

            await _operationLogger.LogOperationToDbAsync(new OperationLog
            {
                LoggedOperation = "CreateCustomerAppDbInventory", LogType = "Create", LogLocation = "AppDbInventory",
                UserName = User.Identity.Name
            });
            
            return Ok(result);
        }

        [HttpPost("AppDb/update")]
        public async Task<IActionResult> UpdateCustomerAppDbInventory(
            [FromBody] CustomerAppDbInventory customerAppDbInventory)
        {
            var result = await _customerTrackingService.UpdateCustomerAppDbInventoryAsync(customerAppDbInventory);
            if (result == null)
                return BadRequest(new { message = "Error while updating record" });

            await _operationLogger.LogOperationToDbAsync(new OperationLog
            {
                LoggedOperation = "UpdateCustomerAppDbInventory", LogType = "Update", LogLocation = "AppDbInventory",
                UserName = User.Identity.Name,
                AffectedObjectId = result.Id,
            });
            
            return Ok(new { Message = "Record updated successfully", UpdatedEntity = result });
        }

        [HttpGet("AppDb/delete/{id}")]
        public async Task<IActionResult> DeleteCustomerAppDbInventory(int id)
        {
            var result = await _customerTrackingService.DeleteCustomerAppDbInventoryAsync(id);
            if (!result)
                return BadRequest(new { message = "Error while deleting record" });

            await _operationLogger.LogOperationToDbAsync(new OperationLog
            {
                LoggedOperation = "DeleteCustomerAppDbInventory", LogType = "Delete", LogLocation = "AppDbInventory",
                UserName = User.Identity.Name,
                AffectedObjectId = id
            });
            
            return Ok(new { message = "Record deleted successfully" });
        }

        #endregion

        #region CustomerExternalUrl Region

        [HttpGet("ExternalUrl/")]
        public async Task<IActionResult> GetAllCustomerExternalUrls()
        {
            var result = await _customerTrackingService.GetAllCustomerExternalUrlsAsync();
            if (result == null)
                return NotFound(new { message = "No records found" });

            return Ok(result);
        }

        [HttpGet("ExternalUrl/{id}")]
        public async Task<IActionResult> GetCustomerExternalUrlById(int id)
        {
            var result = await _customerTrackingService.GetCustomerExternalUrlAsync(id);
            if (result == null)
                return NotFound(new { message = "No records found" });

            return Ok(result);
        }

        [HttpPost("ExternalUrl/")]
        public async Task<IActionResult> CreateCustomerExternalUrl([FromBody] CustomerExternalUrl customerExternalUrl)
        {
            var result = await _customerTrackingService.AddCustomerExternalUrlAsync(customerExternalUrl);
            if (result == null)
                return BadRequest(new { message = "Error while creating record" });

            await _operationLogger.LogOperationToDbAsync(new OperationLog
            {
                LoggedOperation = "CreateCustomerExternalUrl", LogType = "Create", LogLocation = "ExternalUrl",
                UserName = User.Identity.Name,
                AffectedObjectId = result.Id
            });

            return Ok(result);
        }

        [HttpPost("ExternalUrl/update")]
        public async Task<IActionResult> UpdateCustomerExternalUrl([FromBody] CustomerExternalUrl customerExternalUrl)
        {
            var result = await _customerTrackingService.UpdateCustomerExternalUrlAsync(customerExternalUrl);
            if (result == null)
                return BadRequest(new { message = "Error while updating record" });

            await _operationLogger.LogOperationToDbAsync(new OperationLog
            {
                LoggedOperation = "UpdateCustomerExternalUrl", LogType = "Update", LogLocation = "ExternalUrl",
                UserName = User.Identity.Name,
                AffectedObjectId = result.Id
            });

            return Ok(new { Message = "Record updated successfully", UpdatedEntity = result });
        }

        [HttpGet("ExternalUrl/delete/{id}")]
        public async Task<IActionResult> DeleteCustomerExternalUrl(int id)
        {
            var result = await _customerTrackingService.DeleteCustomerExternalUrlAsync(id);
            if (!result)
                return BadRequest(new { message = "Error while deleting record" });

            await _operationLogger.LogOperationToDbAsync(new OperationLog
            {
                LoggedOperation = "DeleteCustomerExternalUrl", LogType = "Delete", LogLocation = "ExternalUrl",
                UserName = User.Identity.Name,
                AffectedObjectId = id
            });
            
            return Ok(new { message = "Record deleted successfully" });
        }

        #endregion

        #region CustomerInternalUrl Region

        [HttpGet("InternalUrl/")]
        public async Task<IActionResult> GetAllCustomerInternalUrls()
        {
            var result = await _customerTrackingService.GetAllCustomerInternalUrlsAsync();
            if (result == null)
                return NotFound(new { message = "No records found" });

            return Ok(result);
        }

        [HttpGet("InternalUrl/{id}")]
        public async Task<IActionResult> GetCustomerInternalUrlById(int id)
        {
            var result = await _customerTrackingService.GetCustomerInternalUrlAsync(id);
            if (result == null)
                return NotFound(new { message = "No records found" });

            return Ok(result);
        }

        [HttpPost("InternalUrl/")]
        public async Task<IActionResult> CreateCustomerInternalUrl([FromBody] CustomerInternalUrl customerInternalUrl)
        {
            var result = await _customerTrackingService.AddCustomerInternalUrlAsync(customerInternalUrl);
            if (result == null)
                return BadRequest(new { message = "Error while creating record" });

            await _operationLogger.LogOperationToDbAsync(new OperationLog
            {
                LoggedOperation = "CreateCustomerInternalUrl", LogType = "Create", LogLocation = "InternalUrl",
                UserName = User.Identity.Name,
                AffectedObjectId = result.Id
            });
            
            return Ok(result);
        }

        [HttpPost("InternalUrl/update")]
        public async Task<IActionResult> UpdateCustomerInternalUrl([FromBody] CustomerInternalUrl customerInternalUrl)
        {
            var result = await _customerTrackingService.UpdateCustomerInternalUrlAsync(customerInternalUrl);
            if (result == null)
                return BadRequest(new { message = "Error while updating record" });

            await _operationLogger.LogOperationToDbAsync(new OperationLog
            {
                LoggedOperation = "UpdateCustomerInternalUrl", LogType = "Update", LogLocation = "InternalUrl",
                UserName = User.Identity.Name,
                AffectedObjectId = result.Id
            });
            
            return Ok(new { Message = "Record updated successfully", UpdatedEntity = result });
        }

        [HttpGet("InternalUrl/delete/{id}")]
        public async Task<IActionResult> DeleteCustomerInternalUrl(int id)
        {
            var result = await _customerTrackingService.DeleteCustomerInternalUrlAsync(id);
            if (!result)
                return BadRequest(new { message = "Error while deleting record" });

            await _operationLogger.LogOperationToDbAsync(new OperationLog
            {
                LoggedOperation = "DeleteCustomerInternalUrl", LogType = "Delete", LogLocation = "InternalUrl",
                UserName = User.Identity.Name,
                AffectedObjectId = id
            });
            
            return Ok(new { message = "Record deleted successfully" });
        }

        #endregion
    }
}