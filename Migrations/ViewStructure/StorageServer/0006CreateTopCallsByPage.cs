using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.ViewStructure.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZZ0006-CreateTopCallsByPage_View")]
    public class CreateTopCallsByPage : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string viewCreateQuery = @"
CREATE VIEW TopCallsByPage
AS
SELECT
    WAL.RequestUrl,
    WAL.LogTimeStamp,
    COUNT(*) AS RequestCount
FROM
    dbo.WebAccessLogs AS WAL WITH (NOLOCK)
WHERE
    UserName IS NOT NULL
GROUP BY
    WAL.RequestUrl,
    WAL.LogTimeStamp
ORDER BY
    RequestCount DESC
";
            migrationBuilder.Sql(sql: viewCreateQuery);
        }
    }
}
