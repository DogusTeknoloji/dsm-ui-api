using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0022-CreateSpDTIISMSHCGetConnectionString_StoredProcedure")]
    public class CreateSpDTIISMSHCGetConnectionString : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Gets the connection string from table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMSHC_GetConnectionString]
    @Id INT
AS
BEGIN
    SELECT 
        * 
    FROM 
        dbo.IISSiteConnectionString 
    WHERE 
        Id =  @Id
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
