using System;

namespace DSM.UI.Api.Models.DatabasePortal
{
    public class EnvanterSQL
    {
        public string InstanceName { get; set; }
        public string ServerName { get; set; }
        public string Domain { get; set; }
        public string Enviroment { get; set; }
        public string AktifPasif { get; set; }
        public string Instance { get; set; }
        public int ID { get; set; }
        public int ListeningPort { get; set; }
        public string FileMonitoring { get; set; }
        public string FileGroupMonitoring { get; set; }
        public string BackupMonitoring { get; set; }
        public string IpAddress { get; set; }
        public string Aciklama { get; set; }
        public string FileMonitoringHata { get; set; }
        public string ConnectionScript { get; set; }
        public string FileGroupMonitoringHata { get; set; }
        public string BackupMonitoringHata { get; set; }
        public string Owner { get; set; }
        public int Logical_CPU_Count { get; set; }
        public int Physical_CPU_Count { get; set; }
        public int Physical_Memory_in_MB { get; set; }
        public string Version { get; set; }
        public string EditionInstalled { get; set; }
        public string ProductBuildLevel { get; set; }
        public string SPLevel { get; set; }
        public string Collaction_Type { get; set; }
        public string IsClustered { get; set; }
        public string virtual_machine_type { get; set; }
        public string OSVersion { get; set; }
        public string CPUName { get; set; }
        public string BackupPolicy { get; set; }
        public string BackupPolicyHata { get; set; }
        public string BackupHistory { get; set; }
        public string BackupHistoryHata { get; set; }
        public string BackupAciklama { get; set; }
        public string Location { get; set; }
        public string JobMonitoring { get; set; }
        public string JobMonitoringHata { get; set; }
        public string DBSearch { get; set; }
        public string DBSearchHata { get; set; }
        public string AGInstalled { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public byte ManuelPatch { get; set; }
        public string SQL { get; set; }
    }
}
