using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0024-CreateSpDTIISMSHCGetListConnectionString_StoredProcedure")]
    public class CreateSpDTIISMSHCGetListConnectionString : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Gets List of Connection Strings by Site from table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMSHC_GetListConnectionString] 
    @SiteId INT 
AS
BEGIN
    SELECT 
        * 
    FROM 
        dbo.IISSiteConnectionString 
    WHERE 
        SiteId = @SiteId
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
