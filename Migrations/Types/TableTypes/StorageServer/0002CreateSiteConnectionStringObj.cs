using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.Types.TableTypes.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZW0002-CreateSiteConnectionStringObj_Type")]
    public class CreateSiteConnectionStringObj : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string typeCreateQuery = @"
CREATE TYPE SiteConnectionStringObj AS TABLE
(
    [SiteId] [bigint] NULL,
    [RawConnectionString] [varchar](512) NULL,
    [ServerName] [varchar](200) NULL,
    [Port] [int] NULL,
    [DatabaseName] [varchar](200) NULL,
    [UserName] [varchar](200) NULL,
    [Password] [varchar](200) NULL,
    [IsAvailable] [bit] NULL,
    [ResponseTime] [bigint] NULL,
    [LastCheckTime] [datetime] NULL,
    [DeleteDate] [datetime] NULL,
    [SendAlertMailWhenUnavailable] [bit] NULL,
    [ConnectionName] [varchar](256) NULL
)
";
            migrationBuilder.Sql(sql: typeCreateQuery);
        }
    }
}
