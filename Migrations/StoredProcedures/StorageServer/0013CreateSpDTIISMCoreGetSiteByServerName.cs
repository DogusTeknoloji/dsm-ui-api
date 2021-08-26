using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0013-CreateSpDTIISMCoreGetSiteByServerName_StoredProcedure")]
    public class CreateSpDTIISMCoreGetSiteByServerName : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Gets sites by ServerName
-- =============================================
CREATE PROCEDURE SP_DTIISMCore_GetSiteByServerName
    @ServerName NVARCHAR(100)
AS
BEGIN
    SELECT 
        * 
    FROM 
        dbo.IISSite 
    WHERE 
        MachineName = @ServerName
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
