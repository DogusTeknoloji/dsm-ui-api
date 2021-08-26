using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0027-CreateSpDTIISMSHCListAllEndpoints_StoredProcedure")]
    public class CreateSpDTIISMSHCListAllEndpoints : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Gets list of EndPoints from table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMSHC_ListAllEndpoints]
AS
BEGIN
    SELECT 
        * 
    FROM 
        dbo.IISSiteEndpoint
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
