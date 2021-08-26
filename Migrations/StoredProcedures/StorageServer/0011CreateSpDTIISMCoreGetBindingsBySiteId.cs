using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0011-CreateSpDTIISMCoreGetBindingsBySiteId_StoredProcedure")]
    public class CreateSpDTIISMCoreGetBindingsBySiteId : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Gets bindings by id from table
-- =============================================
CREATE PROCEDURE SP_DTIISMCore_GetBindingBySiteId
    @SiteId INT
AS
BEGIN
    SELECT 
        * 
    FROM 
        dbo.IISSiteBinding 
    WHERE 
        SiteId = @SiteId
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
