using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0012-CreateSpDTIISMCoreGetSiteById_StoredProcedure")]
    public class CreateSpDTIISMCoreGetSiteById : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	gets site by id from table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMCore_GetSiteById]
    @Id INT
AS
BEGIN
    SELECT 
        * 
    FROM 
        dbo.IISSite 
    WHERE 
        Id = @Id
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
