using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0025-CreateSpDTIISMSHCGetListEndpoint_StoredProcedure")]
    public class CreateSpDTIISMSHCGetListEndpoint : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Gets list of endpoints by SiteId from table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMSHC_GetListEndpoint]
    @SiteId INT
AS
BEGIN
    SELECT 
        * 
    FROM 
        dbo.IISSiteEndpoint 
    WHERE 
        SiteId = @SiteId
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
