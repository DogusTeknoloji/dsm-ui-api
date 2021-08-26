using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.ViewStructure.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZZ0007-CreateTopCallsByUser_View")]
    public class CreateTopCallsByUser : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string viewCreateQuery = @"
CREATE VIEW TopCallsByUser
AS
SELECT
    WAL.UserName,
    WAL.LogTimeStamp,
    COUNT(*) AS RequestCount
FROM
    dbo.WebAccessLogs AS WAL WITH (NOLOCK)
WHERE
    UserName IS NOT NULL
GROUP BY
    WAL.UserName,
    WAL.LogTimeStamp
ORDER BY
    RequestCount DESC
";
            migrationBuilder.Sql(sql: viewCreateQuery);
        }
    }
}
