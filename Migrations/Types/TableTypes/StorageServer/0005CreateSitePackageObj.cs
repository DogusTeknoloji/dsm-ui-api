using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.Types.TableTypes.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZW0005-CreatesitePackageObj_Type")]
    public class CreateSitePackageObj : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string typeCreateQuery = @"
CREATE TYPE SitePackageObj AS TABLE
(
    [SiteId] [bigint] NULL,
    [Name] [varchar](100) NULL,
    [NewVersion] [varchar](50) NULL
)
";
            migrationBuilder.Sql(sql: typeCreateQuery);
        }
    }
}
