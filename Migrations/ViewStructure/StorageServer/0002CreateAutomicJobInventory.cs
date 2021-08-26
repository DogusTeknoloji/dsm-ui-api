using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.ViewStructure.StorageServer
{

    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZZ0002-CreateAutomicJobInventory_View")]
    public class CreateAutomicJobInventory : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string viewCreateQuery = @"
CREATE VIEW AutomicJobInventory
AS
SELECT * FROM DTServer.dbo.AutomicProdEnvanter
";
            migrationBuilder.Sql(sql: viewCreateQuery);
        }
    }
}
