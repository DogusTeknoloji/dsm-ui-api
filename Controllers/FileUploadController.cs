using System.Threading.Tasks;
using DSM.UI.Api.Models.UploadedFiles;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> FTPUploadFile(CreateUploadFileDetailDto detail,IFormFile file)
        {
            var result = await _fileUploadService.FTPUploadFileAsync(detail,file, User.Identity.Name);
            return Ok(result);
        }
    }
}