using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0007-CreateSpDTIISMCoreAddNewWebConfig_StoredProcedure")]
    public class CreateSpDTIISMCoreAddNewWebConfig : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Adds new Web.Config to table
-- =============================================
CREATE PROCEDURE SP_DTIISMCore_AddNewWebConfig
   @SiteId INT,
   @ContentRaw NVARCHAR(MAX)
AS
BEGIN
    DECLARE @CNT INT
    SELECT 
        @CNT = COUNT(*) 
    FROM 
        dbo.IISSiteWebConfiguration
    WHERE 
        Id = @SiteId
    
    IF @CNT > 0
        UPDATE 
            dbo.IISSiteWebConfiguration
        SET 
            ContentRaw = @ContentRaw
        OUTPUT 
            INSERTED.[Id]
        WHERE 
            Id = @SiteId
    ELSE
        INSERT INTO 
            dbo.IISSiteWebConfiguration 
            (Id,ContentRaw)
        OUTPUT 
            INSERTED.[Id]
        VALUES
            (@SiteId,@ContentRaw)
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
