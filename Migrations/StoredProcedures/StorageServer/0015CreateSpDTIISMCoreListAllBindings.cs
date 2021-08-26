using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0015-CreateSpDTIISMCoreListAllBindings_StoredProcedure")]
    public class CreateSpDTIISMCoreListAllBindings : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Gets All bindings of Table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMCore_ListAllBindings]
AS
BEGIN
    SELECT 
        * 
    FROM 
        dbo.IISSiteBinding
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
