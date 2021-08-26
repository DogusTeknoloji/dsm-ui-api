using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Migrations.ViewStructure.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZZ0003-CreateDBServers_View")]
    public class CreateDBServers : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string viewCreateQuery = @"
CREATE VIEW DBServers
AS
SELECT
    ID, ServerName, Owner, Environment, IpAddress AS 'IP_Address',
    ListeningPort AS 'Port', Aciklama AS 'Description', Logical_CPU_Count, Physical_CPU_Count, Physical_Memory_in_MB AS 'Memory',
    OSVersion, Version, virtual_machine_type as 'MachineType', RecordDate, DeleteDate, AktifPasif,
    1 AS DBType, Domain, instance, FileMonitoring, FileGroupMonitoring,
    BackupMonitoring, FileMonitoringHata, ConnectionScript, FileGroupMonitoringHata, BackupMonitoringHata,
    EditionInstalled, ProductBuildLevel, SPLevel, Collation_Type, IsClustered,
    CPUName, BackupPolicy, BackupPolicyHata, BackupHistory, BackupHistoryHata,
    BackupAciklama, Location, JobMonitoring, JobMonitoringHata, DBSearch,
    DBSearchHata, AGInstalled, ManuelPatch, SQLBit, NULL AS Service_Name,
    NULL AS ASM
FROM
    DTEKSQLMNT.DTDBA.dbo.EnvanterSQL
UNION SELECT
    ID, Host AS 'ServerName', Owner, Environment, IP_Address,
    Port, Açıklama AS 'Description', Logical_CPU as Logical_CPU_Count, Physical_CPU as Physical_CPU_Count, Memory,
    Operating_System as OSVersion, Oracle_Version AS Version, MachineType, RecordDate, DeteleDate AS 'DeleteDate',
    AktifPasif, 2 AS DBType, Service_Name, ASM, NULL AS Domain, NULL AS instance, 
    NULL AS FileMonitoring, NULL AS FileGroupMonitoring, NULL AS BackupMonitoring, NULL AS FileMonitoringHata, NULL AS ConnectionScript,
    NULL AS FileGroupMonitoringHata, NULL AS BackupMonitoringHata, NULL AS EditionInstalled, NULL AS ProductBuildLevel, NULL AS SPLevel,
    NULL AS Collation_Type, NULL AS IsClustered, NULL AS CPUName, NULL AS BackupPolicy, NULL AS BackupPolicyHata,
    NULL AS BackupHistory, NULL AS BackupHistoryHata, NULL AS BackupAciklama, NULL AS Location, NULL AS JobMonitoring,
    NULL AS JobMonitoringHata, NULL AS DBSearch, NULL AS DBSearchHata, NULL AS AGInstalled, NULL AS ManuelPatch,
    NULL AS SQLBit
FROM
    DTEKSQLMNT.DTDBA.dbo.EnvanterOracle
UNION SELECT
    ID, Host AS ServerName, Owner, Environment, IP_Address,
    Port, [Description], Logical_CPU as Logical_CPU_Count, Physical_CPU as Physical_CPU_Count, Memory,
    Operating_System as OSVersion, 'Postgres '+ Postgres_Version AS Version, MachineType, RecordDate, DeteleDate,
    AktifPasif, 3 AS DBType, Service_Name, ASM, NULL AS Domain, NULL AS instance, 
    NULL AS FileMonitoring, NULL AS FileGroupMonitoring, NULL AS BackupMonitoring, NULL AS FileMonitoringHata, NULL AS ConnectionScript,
    NULL AS FileGroupMonitoringHata, NULL AS BackupMonitoringHata, NULL AS EditionInstalled, NULL AS ProductBuildLevel, NULL AS SPLevel,
    NULL AS Collation_Type, NULL AS IsClustered, NULL AS CPUName, NULL AS BackupPolicy, NULL AS BackupPolicyHata,
    NULL AS BackupHistory, NULL AS BackupHistoryHata, NULL AS BackupAciklama, NULL AS Location, NULL AS JobMonitoring,
    NULL AS JobMonitoringHata, NULL AS DBSearch, NULL AS DBSearchHata, NULL AS AGInstalled, NULL AS ManuelPatch,
    NULL AS SQLBit
FROM
    DTEKSQLMNT.DTDBA.dbo.EnvanterOpenSource
";
            migrationBuilder.Sql(sql: viewCreateQuery);
        }
    }
}
