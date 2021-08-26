using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0006-CreateSpDTIISMCoreAddNewSiteMultiple_StoredProcedure")]
    public class CreateSpDTIISMCoreAddNewSiteMultiple : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Add new site to table
-- =============================================
CREATE PROCEDURE SP_DTIISMCore_AddNewSiteMultiple
    @Table SiteObj READONLY
AS
BEGIN
    MERGE dbo.IISSite AS SourceSite USING @Table AS DestSite
        ON (SourceSite.Name = DestSite.Name AND SourceSite.MachineName = DestSite.MachineName)
    WHEN MATCHED THEN UPDATE 
        SET 
            IISSiteId = DestSite.IISSiteId, MachineName = DestSite.MachineName, Name= DestSite.Name, ApplicationPoolName = DestSite.ApplicationPoolName, PhysicalPath= DestSite.PhysicalPath, 
            EnabledProtocols= DestSite.EnabledProtocols, MaxBandwitdh= DestSite.MaxBandwitdh, MaxConnections= DestSite.MaxConnections, LogFileEnabled= DestSite.LogFileEnabled, LogFileDirectory= DestSite.LogFileDirectory, 
            LogFormat= DestSite.LogFormat, LogPeriod= DestSite.LogPeriod, ServerAutoStart= DestSite.ServerAutoStart, State= DestSite.State, TraceFailedRequestsLoggingEnabled= DestSite.TraceFailedRequestsLoggingEnabled,
            TraceFailedRequestsLoggingDirectory= DestSite.TraceFailedRequestsLoggingDirectory, LastUpdated= DestSite.LastUpdated, DateDeleted= DestSite.DateDeleted, NetFrameworkVersion = DestSite.NetFrameworkVersion, IsAvailable = DestSite.IsAvailable,
            LastCheckTime = DestSite.LastCheckTime, SendAlertMailWhenUnavailable = DestSite.SendAlertMailWhenUnavailable, AppType =  DestSite.AppType, WebConfigLastBackupDate =  DestSite.WebConfigLastBackupDate, WebConfigBackupDirectory =  DestSite.WebConfigBackupDirectory
    WHEN NOT MATCHED BY TARGET THEN INSERT 
        ( ApplicationPoolName, AppType, DateDeleted, EnabledProtocols, IISSiteId, 
          IsAvailable, LastCheckTime, LastUpdated, LogFileDirectory, LogFileEnabled, 
          LogFormat, LogPeriod, MachineName, MaxBandwitdh, MaxConnections, 
          Name, NetFrameworkVersion, PhysicalPath, SendAlertMailWhenUnavailable, ServerAutoStart, 
          State, TraceFailedRequestsLoggingDirectory, TraceFailedRequestsLoggingEnabled, WebConfigBackupDirectory, WebConfigLastBackupDate)
        VALUES 
        (DestSite.ApplicationPoolName, DestSite.AppType, DestSite.DateDeleted, DestSite.EnabledProtocols, DestSite.IISSiteId, 
        DestSite.IsAvailable, DestSite.LastCheckTime, DestSite.LastUpdated, DestSite.LogFileDirectory, DestSite.LogFileEnabled, 
        DestSite.LogFormat, DestSite.LogPeriod, DestSite.MachineName, DestSite.MaxBandwitdh, DestSite.MaxConnections, 
        DestSite.Name, DestSite.NetFrameworkVersion, DestSite.PhysicalPath, DestSite.SendAlertMailWhenUnavailable, DestSite.ServerAutoStart, 
        DestSite.State, DestSite.TraceFailedRequestsLoggingDirectory, DestSite.TraceFailedRequestsLoggingEnabled, DestSite.WebConfigBackupDirectory, DestSite.WebConfigLastBackupDate);

    SELECT 
        Id, ApplicationPoolName, AppType, DateDeleted, EnabledProtocols, IISSiteId,
        IsAvailable, LastCheckTime, LastUpdated, LogFileDirectory, LogFileEnabled, 
        LogFormat, LogPeriod, MachineName, MaxBandwitdh, MaxConnections, 
        Name, NetFrameworkVersion, PhysicalPath, SendAlertMailWhenUnavailable, ServerAutoStart, 
        State, TraceFailedRequestsLoggingDirectory, TraceFailedRequestsLoggingEnabled, WebConfigBackupDirectory, WebConfigLastBackupDate 
    FROM 
        dbo.IISSite 
    WHERE 
        Name IN (SELECT Name FROM @Table)
    RETURN
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
