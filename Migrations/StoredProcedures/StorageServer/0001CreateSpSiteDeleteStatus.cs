using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0001-CreateSpSiteDeleteStatus_StoredProcedure")]
    public class CreateSpSiteDeleteStatus : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
CREATE PROCEDURE SP_SITE_DELETE_STATUS
AS
BEGIN
    UPDATE 
        dbo.IISSite
    SET
        DateDeleted = Sites.LastUpdated
    FROM
        dbo.Servers AS Srv WITH (NOLOCK)
            INNER JOIN dbo.IISSite AS Sites
                ON Sites.MachineName = SUBSTRING(Srv.HostName,0,CHARINDEX('.', Srv.HostName))
            INNER JOIN 
                (SELECT
                    ServerName, Max(LastUpdated) AS LastUpdate
                 FROM
                    dbo.Servers AS Srv WITH (NOLOCK)
                        INNER JOIN dbo.IISSite AS Sites
                            ON Sites.MachineName = SUBSTRING(Srv.HostName,0,CHARINDEX('.', Srv.HostName))
                 GROUP BY
                    ServerName) AS TServer
    WHERE
        DATEDIFF(DAY, Sites.LastUpdated, TServer.LastUpdate) > 1
            OR Srv.PowerState = 0

    UPDATE
        dbo.IISSite
    SET
        DateDeleted = ST.LastUpdated
    FROM
        dbo.IISSite AS ST WITH (NOLOCK)
            LEFT OUTER JOIN dbo.Servers AS SRV WITH (NOLOCK)
                ON ST.MachineName = SUBSTRING( SRV.HostName, 0, CHARINDEX( '.', SRV.HostName))
    WHERE
        SRV.ServerName IS NULL
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
