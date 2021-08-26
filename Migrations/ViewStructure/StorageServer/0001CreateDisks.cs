using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.ViewStructure.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZZ0001-CreateDisks_View")]
    public class CreateDisks : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string viewCreateQuery = @"
CREATE VIEW Disks
AS
SELECT
    DU.DiskId, DU.DiskName, DU.DiskCapacity, DU.DiskFreeSpace, DU.ServerId
FROM
    DSMVCenter.dbo.Disks AS DU
WHERE
    DU.DiskCapacity > 0
";
            migrationBuilder.Sql(sql: viewCreateQuery);
        }
    }
}
