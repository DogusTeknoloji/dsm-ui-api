using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.Types.TableTypes.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZW0004-CreateSiteObj_Type")]
    public class CreateSiteObj : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string typeCreateQuery = @"
CREATE TYPE SiteObj AS TABLE
(
    [IISSiteId] [bigint] NULL,
    [MachineName] [nvarchar](100) NULL,
    [Name] [nvarchar](250) NULL,
    [ApplicationPoolName] [nvarchar](100) NULL,
    [PhysicalPath] [nvarchar](512) NULL,
    [EnabledProtocols] [nvarchar](100) NULL,
    [MaxBandwitdh] [bigint] NULL,
    [MaxConnections] [bigint] NULL,
    [LogFileEnabled] [bit] NULL,
    [LogFileDirectory] [nvarchar](256) NULL,
    [LogFormat] [nvarchar](50) NULL,
    [LogPeriod] [nvarchar](50) NULL,
    [ServerAutoStart] [bit] NULL,
    [State] [nvarchar](72) NULL,
    [TraceFailedRequestsLoggingEnabled] [bit] NULL,
    [TraceFailedRequestsLoggingDirectory] [nvarchar](512) NULL,
    [LastUpdated] [datetime2](7) NULL,
    [DateDeleted] [datetime2](7) NULL,
    [WebConfigBackupDirectory] [varchar](200) NULL,
    [WebConfigLastBackupDate] [datetime2](7) NULL,
    [AppType] [varchar](30) NULL,
    [IsAvailable] [bit] NULL,
    [LastCheckTime] [datetime2](7) NULL,
    [SendAlertMailWhenUnavailable] [bit] NULL,
    [NetFrameworkVersion] [nvarchar](50) NULL
)
";
            migrationBuilder.Sql(sql: typeCreateQuery);
        }
    }
}
