using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0017-CreateSpDTIISMCoreListAllWebConfigs_StoredProcedure")]
    public class CreateSpDTIISMCoreListAllWebConfigs : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Gets all Site Web Configs
-- =============================================
CREATE PROCEDURE SP_DTIISMCore_ListAllWebConfigs
AS
BEGIN
    SELECT 
        * 
    FROM 
        dbo.IISSiteWebConfiguration
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
