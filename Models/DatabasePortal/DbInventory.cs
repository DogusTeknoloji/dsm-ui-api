using System;

namespace DSM.UI.Api.Models.DatabasePortal
{
    public class DbInventory
    {
        public int Id { get; set; }
        public string ServerName { get; set; }
        public string Owner { get; set; }
        public string Environment { get; set; }
        public string IpAddress { get; set; }
        public int? Port { get; set; }
        public string Description { get; set; }
        public int? LogicalCpuCount { get; set; }
        public int? PhysicalCpuCount { get; set; }
        public int? Memory { get; set; }
        public string Osversion { get; set; }
        public string Version { get; set; }
        public string MachineType { get; set; }
        public DateTime? RecordDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string AktifPasif { get; set; }
        public int Dbtype { get; set; }
        public string Domain { get; set; }
        public string Instance { get; set; }
        public string FileMonitoring { get; set; }
        public string FileGroupMonitoring { get; set; }
        public string BackupMonitoring { get; set; }
        public string FileMonitoringHata { get; set; }
        public string ConnectionScript { get; set; }
        public string FileGroupMonitoringHata { get; set; }
        public string BackupMonitoringHata { get; set; }
        public string EditionInstalled { get; set; }
        public string ProductBuildLevel { get; set; }
        public string Splevel { get; set; }
        public string CollationType { get; set; }
        public string IsClustered { get; set; }
        public string Cpuname { get; set; }
        public string BackupPolicy { get; set; }
        public string BackupPolicyHata { get; set; }
        public string BackupHistory { get; set; }
        public string BackupHistoryHata { get; set; }
        public string BackupAciklama { get; set; }
        public string Location { get; set; }
        public string JobMonitoring { get; set; }
        public string JobMonitoringHata { get; set; }
        public string Dbsearch { get; set; }
        public string DbsearchHata { get; set; }
        public string Aginstalled { get; set; }
        public byte? ManuelPatch { get; set; }
        public string Sqlbit { get; set; }
        public int? ServiceName { get; set; }
        public int? Asm { get; set; }
    }
}
