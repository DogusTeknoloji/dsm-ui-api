using System.Linq;
using System.Threading.Tasks;
using DSM.UI.Api.Models.UploadedFiles;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace DSM.UI.Api.Controllers
{
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }
        
        [HttpPost("FTPUploadFile/")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public async Task<IActionResult> FTPUploadFile(CreateUploadFileDetailDto detail,IFormFile file)
        {
            var result = await _fileUploadService.FTPUploadFileAsync(detail,file, User.Identity.Name);
            return Ok(result);
        }
        
        [HttpGet("FTPDownloadFileWithFileName/{fileName}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> FTPDownloadFile(string fileName)
        {
            var result = _fileUploadService.FTPDownloadFile(fileName, User.Identity.Name);
            
            var fileProvider = new FileExtensionContentTypeProvider();
            fileProvider.TryGetContentType(fileName, out var contentType);
            
            return File(result, contentType ?? "application/octet-stream", fileName);
        }
        
        [HttpGet("FTPFiles/")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> FTPGetAllFiles()
        {
            var result = await _fileUploadService.GetAllFilesAsync();

            return result.Any() ? (IActionResult)Ok(result) : NotFound();
        }
        
        [HttpGet("FTPFiles/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> FTPGetFile(int id)
        {
            var result = await _fileUploadService.GetFileByIdAsync(id);

            return result != null ? (IActionResult)Ok(result) : NotFound();
        }
        
        [HttpGet("FTPDeleteFile/{id}")]
        [Authorize(Roles = "Administrator, CIFANG")]
        public async Task<IActionResult> FTPDeleteFile(int id)
        {
            var result = await _fileUploadService.FTPDeleteFile(id, User.Identity.Name);

            return result ? (IActionResult)Ok(result) : NotFound();
        }
     
    }
}