using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0008-CreateSpDTIISMCoreAddNewWebConfigMultiple_StoredProcedure")]
    public class CreateSpDTIISMCoreAddNewWebConfigMultiple : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author:		Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Adds new Web.Config to table
-- =============================================
CREATE PROCEDURE SP_DTIISMCore_AddNewWebConfigMultiple
    @Table SiteWebConfigObj READONLY
AS
BEGIN
    MERGE dbo.IISSiteWebConfiguration AS SourceConfig USING @Table AS DestConfig
         ON (SourceConfig.Id = DestConfig.Id)
            WHEN MATCHED THEN UPDATE
                 SET 
                    ContentRaw = DestConfig.ContentRaw
            WHEN NOT MATCHED BY TARGET THEN INSERT
                    (Id,ContentRaw)
            VALUES 
                    (DestConfig.Id,DestConfig.ContentRaw);

    SELECT 
        * 
    FROM 
        dbo.IISSiteWebConfiguration 
    WHERE 
        Id IN(SELECT Id FROM @Table)
	RETURN
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
