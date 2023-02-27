using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.Types.TableTypes.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZW0006-CreatesitePackageObj_Type")]
    public class CreateSiteWebConfigObj : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string typeCreateQuery = @"
CREATE TYPE SiteWebConfigObj AS TABLE
(
    [Id] [bigint] NULL,
    [ContentRaw] [text] NULL
)
";
            migrationBuilder.Sql(sql: typeCreateQuery);
        }
    }
}
