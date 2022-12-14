using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DSM.UI.Api.Helpers;
using DSM.UI.Api.Helpers.CustomHelpers;
using DSM.UI.Api.Models.LogModels;
using DSM.UI.Api.Models.UploadedFiles;
using DSM.UI.Api.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DSM.UI.Api.Services
{
    public interface IFileUploadService
    {
        Task<UploadedFileDetail> FTPUploadFileAsync(CreateUploadFileDetailDto uploadedFileDetail, IFormFile file, string UserName);
    }

    public class FileUploadService : IFileUploadService
    {
        private readonly DSMStorageDataContext _context;
        private readonly FTPSettings _ftpSettings;
        private readonly IDSMOperationLogger _logger;

        public FileUploadService(DSMStorageDataContext context, IOptions<FTPSettings> ftpSettings,
            IDSMOperationLogger logger)
        {
            _context = context;
            _logger = logger;
            _ftpSettings = ftpSettings.Value;
        }

        public async Task<UploadedFileDetail> FTPUploadFileAsync(CreateUploadFileDetailDto uploadedFileDetail, IFormFile file, string UserName)
        {
            
            var newFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.Now.FullDateAndTimeStringWithUnderscore()}{Path.GetExtension(file.FileName)}";
            var request = (FtpWebRequest)WebRequest.Create($"ftp://{_ftpSettings.Host}/{_ftpSettings.UploadPath}/{newFileName}");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(_ftpSettings.UserName, _ftpSettings.Password);

            var fileStream = file.OpenReadStream();

            var requestStream = request.GetRequestStream();
            await fileStream.CopyToAsync(requestStream);
            requestStream.Close();
            fileStream.Close();

            var fileSize = FileOperationHelper.CalculateFileSize(file.Length);

            var newRecord = new UploadedFileDetail
            {
                FileDescription = uploadedFileDetail.FileDescription,
                FileSize = fileSize,
                FileName = newFileName,
                UploadedBy = UserName,
                FileUploadDate = DateTime.Now,
                FilePath = _ftpSettings.UploadPath,
                FileTitle = uploadedFileDetail.FileTitle,
                FileType = Path.GetExtension(file.FileName).TrimStart('.'),
            };
            
            await _context.UploadedFileDetails.AddAsync(newRecord);
            await _context.SaveChangesAsync();
            
            await _logger.LogOperationToDbAsync(new OperationLog
            {
                LogDate = DateTime.Now,
                LoggedOperation = "File uploaded to FTP",
                LogType = "FTP",
                LogLocation = "FileUploadService",
                UserName = UserName,
                AffectedObjectId = newRecord.Id 
            });
            
            return newRecord;
        }
    }
}