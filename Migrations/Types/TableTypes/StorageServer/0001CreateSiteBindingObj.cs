using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.Types.TableTypes.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZW0001-CreateSiteBindingObj_Type")]
    public class CreateSiteBindingObj : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string typeCreateQuery = @"
CREATE TYPE SiteBindingObj AS TABLE
(
    [Host] [varchar](150) NULL,
    [SiteId] [bigint] NULL,
    [IsSSLBound] [bit] NULL,
    [IpAddress] [varchar](64) NULL,
    [IpAddressFamily] [varchar](50) NULL,
    [Port] [varchar](10) NULL,
    [Protocol] [varchar](40) NULL
)
";
            migrationBuilder.Sql(sql: typeCreateQuery);
        }
    }
}
