using System;

namespace DSM.UI.Api.Models.LogModels
{
    public class OperationLog
    {
        public int Id { get; set; }
        public string LogType { get; set; }
        public string LoggedOperation { get; set; }
        public string LogLocation { get; set; }
        public string UserName { get; set; }
        public DateTime LogDate { get; set; }
        public int AffectedObjectId { get; set; }
    }
}