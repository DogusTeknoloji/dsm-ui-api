using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSM.UI.Api.Models.WebAccessLogs
{
    public class WebAccessLog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string CredentialsIssuer { get; set; }
        public DateTime? CredentialsIssuedAt { get; set; }
        public DateTime? CredentialsRemainingTimeToExpire { get; set; }
        [Required]
        public DateTime LogTimeStamp { get; set; }
        [Required] // DB ye ekle
        public string RequestMethod { get; set; }
        [Required]
        public string RequestUrl { get; set; }
        [Required] // DB ye ekle
        public int ServerResponseCode { get; set; }
        public string QueryString { get; set; }
        public bool IsHttps { get; set; }
        public string Protocol { get; set; }
        public string UserIpAddress { get; set; }
        public int UserPort { get; set; }
        public string DestinationIpAddress { get; set; }
        public int DestinationPort { get; set; }
        public string UserBrowser { get; set; }
    }
}