using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0014-CreateSpDTIISMCoreGetSiteByServerName_StoredProcedure")]
    public class CreateSpDTIISMCoreGetWebConfigById : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Get web.config by id
-- =============================================
CREATE PROCEDURE SP_DTIISMCore_GetWebConfigById
    @Id INT
AS
BEGIN
    SELECT 
        * 
    FROM 
        dbo.IISSiteWebConfiguration 
    WHERE 
        Id = @Id
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
