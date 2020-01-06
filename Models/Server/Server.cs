using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.Server
{
    public class Server
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("VMNAME")]
        public string MachineName { get; set; }
        public string OperatingSystem { get; set; }
        public string DnsName { get; set; }
        //[Column("Company")]
        //public string CmpFix { get; set; }
        public string IpAddress { get; set; }
        public string Responsible { get; set; }
        //public DateTime DateCurrent { get; set; }
        public string LastBackup { get; set; }
        public int MemoryGB { get; set; }
        public int NumCPU { get; set; }
        public int Price { get; set; }
        public string ESX { get; set; }
        public string Cluster { get; set; }
        public int CompanyId { get; set; }
        public virtual Company.Company Company { get; set; }
        public virtual List<ServerDisk> ServerDisks { get; set; }
    }
}
