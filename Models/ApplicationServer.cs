using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSM.UI.Api.Models
{
    public class ApplicationServer
    {
        [Key]
        public int ServerId { get; set; }
        public string Company { get; set; }
        public string ApplicationName { get; set; }
        public string ServerName { get; set; }
        public string ServerIp { get; set; }
        public string Environment { get; set; }
        public string Location { get; set; }
        public string Domain { get; set; }
        public string OperatingSystem { get; set; }
        [Column("Licence")]
        public string License { get; set; }
        public string Responsible { get; set; }
        [Column("LicenceVersion")]
        public string LicenseVersion { get; set; }
        [Column("SubApplicationName")]
        public string SubApplication { get; set; }
        [Column("Owned")]
        public string Owner { get; set; }
        public string NetworkStatus { get; set; }
        public string ESX { get; set; }
        public string Cluster { get; set; }
        public int? MemoryGB { get; set; }
        public int? NumCPU { get; set; }
    }
}
