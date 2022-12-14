using System;

namespace DSM.UI.Api.Models.UploadedFiles
{
    public class UploadedFileDetail
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        public string FileType { get; set; }
        public DateTime FileUploadDate { get; set; }
        public string UploadedBy { get; set; }
        public string FileSize { get; set; }
        public string FileTitle { get; set; }
        public string FilePath { get; set; }
    }
}