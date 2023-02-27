using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0026-CreateSpDTIISMSHCListAllConnectionStrings_StoredProcedure")]
    public class CreateSpDTIISMSHCListAllConnectionStrings : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Gets all Connection Strings
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMSHC_ListAllConnectionStrings]
AS
BEGIN
    SELECT 
        * 
    FROM 
        dbo.IISSiteConnectionString
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
