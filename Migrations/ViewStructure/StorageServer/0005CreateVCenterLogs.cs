using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.ViewStructure.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZZ0005-CreateVCenterLogs_View")]
    public class CreateVCenterLogs : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string viewCreateQuery = @"
CREATE VIEW VCenterLogs
AS
SELECT 
    VL.LogName, VL.LogValue
FROM 
    DSMVCenter.dbo.Logs AS VL
WHERE
    VL.LogId IN 
                (SELECT
                    MAX(VL.LogId)
                 FROM
                    DSMVCenter.dbo.Logs AS VL
                 GROUP BY
                    VL.LogName)
";
            migrationBuilder.Sql(sql: viewCreateQuery);
        }
    }
}
