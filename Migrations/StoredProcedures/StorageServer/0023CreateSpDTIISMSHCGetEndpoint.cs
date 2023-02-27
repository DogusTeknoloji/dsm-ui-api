using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0023-CreateSpDTIISMSHCGetEndpoint_StoredProcedure")]
    public class CreateSpDTIISMSHCGetEndpoint : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Gets Endpoint by id from table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMSHC_GetEndpoint]
    @Id INT
AS
BEGIN
    SELECT 
        * 
    FROM 
        dbo.IISSiteEndpoint 
    WHERE 
        Id = @Id
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
