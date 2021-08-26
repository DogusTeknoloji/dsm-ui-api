using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0005-CreateSpDTIISMCoreAddNewBindingMultiple_StoredProcedure")]
    public class CreateSpDTIISMCoreAddNewSite : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author:		Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Add new site to table
-- =============================================
CREATE PROCEDURE SP_DTIISMCore_AddNewSite
    @IISSiteId BIGINT,
    @Name NVARCHAR(250),
    @MachineName NVARCHAR(100),
    @ApplicationPoolName NVARCHAR(100),
    @PhysicalPath  NVARCHAR(512),
    @EnabledProtocols NVARCHAR(100),
    @MaxBandwitdh BIGINT,
    @MaxConnections BIGINT,
    @LogFileEnabled BIT,
    @LogFileDirectory NVARCHAR(256),
    @LogFormat NVARCHAR(50),
    @LogPeriod NVARCHAR(50),
    @ServerAutoStart BIT,
    @State NVARCHAR(72),
    @TraceFailedRequestsLoggingEnabled BIT,
    @TraceFailedRequestsLoggingDirectory NVARCHAR(512),
    @LastUpdated DATETIME,
    @DateDeleted DATETIME,
    @NetFrameworkVersion NVARCHAR(50),
    @IsAvailable BIT,
    @LastCheckTime DATETIME,
    @SendAlertMailWhenUnavailable BIT,
    @AppType VARCHAR(30),
    @WebConfigLastBackupDate DATETIME,
    @WebConfigBackupDirectory VARCHAR(200)
AS
BEGIN
    DECLARE @CNT INT
    SELECT 
        @CNT = COUNT(*) 
    FROM 
        dbo.IISSite
    WHERE 
        Name = @Name 
            AND  MachineName = @MachineName
    
    IF @CNT > 0
        UPDATE 
            dbo.IISSite 
        SET 
            IISSiteId = @IISSiteId, MachineName = @MachineName, Name= @Name, ApplicationPoolName = @ApplicationPoolName, PhysicalPath= @PhysicalPath, 
            EnabledProtocols= @EnabledProtocols, MaxBandwitdh= @MaxBandwitdh, MaxConnections= @MaxConnections, LogFileEnabled= @LogFileEnabled, LogFileDirectory= @LogFileDirectory, 
            LogFormat= @LogFormat, LogPeriod= @LogPeriod, ServerAutoStart= @ServerAutoStart, State= @State, TraceFailedRequestsLoggingEnabled= @TraceFailedRequestsLoggingEnabled, 
            TraceFailedRequestsLoggingDirectory= @TraceFailedRequestsLoggingDirectory, LastUpdated= @LastUpdated, DateDeleted= @DateDeleted, NetFrameworkVersion = @NetFrameworkVersion, IsAvailable = @IsAvailable, 
            LastCheckTime = @LastCheckTime, SendAlertMailWhenUnavailable = @SendAlertMailWhenUnavailable, AppType = @AppType , WebConfigLastBackupDate = @WebConfigLastBackupDate , WebConfigBackupDirectory =  @WebConfigBackupDirectory
        OUTPUT 
            INSERTED.[Id]
        WHERE 
            Name = @Name 
                AND MachineName = @MachineName
    ELSE
        INSERT INTO 
            dbo.IISSite 
        ( IISSiteId, MachineName, Name, ApplicationPoolName, PhysicalPath,
          EnabledProtocols, MaxBandWitdh, MaxConnections, LogFileEnabled, LogFileDirectory,
          LogFormat ,LogPeriod, ServerAutoStart, State, TraceFailedRequestsLoggingEnabled,
          TraceFailedRequestsLoggingDirectory, LastUpdated, DateDeleted, NetFrameworkVersion, IsAvailable,
          LastCheckTime, SendAlertMailWhenUnavailable, AppType, WebConfigLastBackupDate, WebConfigBackupDirectory)
        OUTPUT 
            INSERTED.[Id]
        VALUES( @IISSiteId, @MachineName, @Name, @ApplicationPoolName, @PhysicalPath,
                @EnabledProtocols, @MaxBandwitdh, @MaxConnections, @LogFileEnabled, @LogFileDirectory,
                @LogFormat, @LogPeriod, @ServerAutoStart, @State, @TraceFailedRequestsLoggingEnabled,
                @TraceFailedRequestsLoggingDirectory, @LastUpdated, @DateDeleted, @NetFrameworkVersion, @IsAvailable,
                @LastCheckTime, @SendAlertMailWhenUnavailable, @AppType, @WebConfigLastBackupDate, @WebConfigBackupDirectory)
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
