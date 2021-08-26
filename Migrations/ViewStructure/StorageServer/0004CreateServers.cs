using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.ViewStructure.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZZ0004-CreateServers_View")]
    public class CreateServers : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string viewCreateQuery = @"
CREATE VIEW Servers
AS
SELECT
    X.ServerId,  X.ServerName, X.HostName, X.IPAddress, x.CustomIp,
    X.PhysicalLocation, X.Responsible, X.ServerType, X.LastBackup, X.OperatingSystem,
    X.Service, X.Boottime, X.TotalCPU, X.TotalMemory, X.MemoryUsage,
    X.ToolsRunningStatus, X.ESXI, X.Cluster, X.Notes, dbo.Companies.CompanyId,
    X.PowerState, X.OdmReplication
FROM 
    (SELECT
         CASE Company
            WHEN 'DOAS' THEN 'Doğuş Otomotiv A.Ş.'
            WHEN 'TUVT' THEN 'Tüvtürk A.Ş.'
            WHEN 'DREAM' THEN 'D.ream A.Ş.'
            WHEN 'LEICA' THEN 'Leica Camera A.G.'
            WHEN 'DHOL' THEN 'Doğuş Holding A.Ş.'
            WHEN 'RELATED' THEN 'Related Marketing Cloud'
            WHEN 'Related Digital' THEN 'Related Marketing Cloud'
            WHEN 'DHOL DHOL' THEN 'Doğuş Holding A.Ş.'
            WHEN 'PERAKENDE' THEN 'Doğuş Perakende A.Ş.'
            WHEN 'DOEN' THEN 'Doğuş Enerji A.Ş.'
            WHEN 'DMS' THEN 'Doğuş Müşteri Sistemleri A.Ş.'
            WHEN 'DMARIN' THEN 'D-Marin'
            WHEN 'Dogus Sigorta' THEN 'Doğuş Sigorta A.Ş.'
            WHEN 'DGYM' THEN 'D-Gym'
            WHEN 'DOTO' THEN 'Doğuş Oto A.Ş.'
            WHEN 'EuroMessage' THEN 'Related Marketing Cloud - Euro Message'
            WHEN 'EUROMSG' THEN 'Related Marketing Cloud - Euro Message'
            WHEN 'DINSAAT' THEN 'Doğuş İnşaat A.Ş.'
            WHEN 'DTEK' THEN 'Doğuş Teknoloji'
            WHEN 'DAVN' THEN 'Doğuş Avenue'
            WHEN 'POZ' THEN 'Pozitif Müzik'
            WHEN 'GYO' THEN 'Doğuş GYO'
            WHEN 'DGYO' THEN 'Doğuş GYO'
            WHEN 'ANTUR' THEN 'Antur Turizm A.Ş.'
            WHEN 'DTURIZM' THEN 'Doğuş Turizm A.Ş.'
            WHEN 'DTG' THEN 'Doğuş Turizm A.Ş.'
            WHEN 'DPG' THEN 'Doğuş Perakende A.Ş.'
            WHEN 'DTEK, DHOL' THEN 'Doğuş Teknoloji'
            WHEN 'DOEN, DMS, DMARIN, DOAS, DHOL' THEN 'Doğuş Teknoloji'
            WHEN 'DOAS.' THEN 'Doğuş Otomotiv A.Ş.'
            WHEN 'DOAS, DHOL' THEN 'Doğuş Otomotiv A.Ş.'
            WHEN 'DOAS, DDR, DHOL' THEN 'Doğuş Holding A.Ş.'
            WHEN 'DDR' THEN 'D.ream A.Ş.'
            WHEN 'VDF' THEN 'Volkswagen Doğuş Finans A.Ş.'
            WHEN ' VDF' THEN 'Volkswagen Doğuş Finans A.Ş.'
            WHEN 'DINSAAT, DHOL' THEN 'Doğuş İnşaat A.Ş.'
            WHEN 'SAMSUNG' THEN 'Samsung Electronics Inc.'
            WHEN 'DT' THEN 'Doğuş Teknoloji'
            WHEN 'DINS' THEN 'Doğuş İnşaat A.Ş.'
            WHEN 'TURIZM' THEN 'Doğuş Turizm A.Ş.'
            WHEN 'EUMSG' THEN 'Related Marketing Cloud - Euro Message'
            WHEN 'KIKO' THEN 'Doğuş Avenue'
            WHEN 'Körfez Havacılık' THEN 'Körfez Havacılık A.Ş.'
            WHEN 'DOME' THEN 'Dome Group LLC.'
            WHEN 'DTURIZM, DHOL' THEN 'Doğuş Turizm A.Ş.'
            WHEN 'DMARIN, DHOL, DTEK, TUVTURK' THEN 'Doğuş Teknoloji'
            WHEN 'DTURIZM, DHOL' THEN 'Doğuş Turizm A.Ş.'
            WHEN 'Moneye' THEN 'Monai Finansal Pazarlama A.Ş.'
            WHEN 'DHOLDING' THEN 'Doğuş Holding A.Ş.'
            WHEN 'ANTUR' THEN 'Antur Turizm A.Ş.'
            WHEN 'DENERJI' THEN 'Doğuş Enerji A.Ş.'
            WHEN 'DINSAAT' THEN 'Doğuş İnşaat A.Ş.'
            WHEN 'DMS' THEN 'Doğuş Müşteri Sistemleri A.Ş.'
            WHEN 'DOGUS PAREKENDE' THEN 'Doğuş Perakende A.Ş.'
            WHEN 'DOGUSPERAKENDE' THEN 'Doğuş Perakende A.Ş.'
            WHEN 'Doğuş Perakende' THEN 'Doğuş Perakende A.Ş.'
            WHEN 'DOTOMOTIV' THEN 'Doğuş Otomotiv A.Ş.'
            WHEN 'DREAM' THEN 'D.ream A.Ş.'
            WHEN 'DT' THEN 'Doğuş Teknoloji'
            WHEN 'Dteknoloji' THEN 'Doğuş Teknoloji'
            WHEN 'EURO MESSAGE' THEN 'Related Marketing Cloud - Euro Message'
            WHEN 'Holding' THEN 'Doğuş Holding A.Ş.'
            WHEN 'POZITIF' THEN 'Pozitif Müzik'
            WHEN 'TUVTURK' THEN 'TüvTürk A.Ş.'
            WHEN 'Doğuş Sigorta' THEN 'Doğuş Sigorta'
            ELSE 'Unknown' END AS CompanyX,
        ServerId, ServerName, HostName, Company, IPAddress,
        CustomIp, PhysicalLocation, Responsible, ServerType, LastBackup,
        OperatingSystem, Boottime, TotalCPU, TotalMemory, MemoryUsage,
        ToolsRunningStatus, ESXI, Cluster, Notes, Service,
        CASE PowerState
            WHEN 'poweredOn' THEN 1
            WHEN 'poweredOff' THEN 0
            ELSE -1 END AS PowerState,
        OdmReplication
    FROM
        (SELECT 
            ServerId, ServerName, HostName, Company, IPAddress,
            CustomIp, PhysicalLocation, Responsible, ServerType, LastBackup,
            OperatingSystem, Boottime, TotalCPU ,TotalMemory, MemoryUsage,
            ToolsRunningStatus, Service ,ESXI, Cluster, Notes,
            PowerState, OdmReplication
         FROM 
            DSMVCenter.dbo.Servers
         UNION SELECT
            ('1433000' + ID) AS ServerId, 
            ISNULL(ServerName,'')  COLLATE Turkish_CI_AS AS ServerName,
            ISNULL((ServerName +'.' + Domain),'')  COLLATE Turkish_CI_AS AS HostName,
            Owner COLLATE Turkish_CI_AS AS Company,
            IP_Address COLLATE Turkish_CI_AS AS IPAddress, NULL AS CustomIp,
            [Location] COLLATE Turkish_CI_AS AS PhysicalLocation,
            'DT Veritabani Yonetimi' AS Responsible,
            Environment COLLATE Turkish_CI_AS AS ServerType,  NULL AS LastBackup,
            OSVersion COLLATE Turkish_CI_AS AS OperatingSystem, NULL AS Boottime,
            (Physical_CPU_Count + Logical_CPU_Count) AS TotalCPU,
            Memory AS TotalMemory,
            NULL AS MemoryUsage,
            'Physical Srv' AS ToolsRunningStatus,
            [Version] COLLATE Turkish_CI_AS AS [Service],
            NULL AS ESXI,
            NULL AS Cluster,
            ('Physical Server, ' + CPUName + ', ' + Description) COLLATE Turkish_CI_AS AS Notes,
            NULL AS  PowerState,
            NULL AS OdmReplication
        FROM
            DSMStorageServer.dbo.DBServers
        WHERE
            MachineType IN ('Cloud', 'Oracle Linux Server Release 7.8', 'physical')
        ) AS T
    ) AS X LEFT OUTER JOIN
         dbo.Companies
            ON X.CompanyX = dbo.Companies.Name
";
            migrationBuilder.Sql(sql: viewCreateQuery);
        }
    }
}
