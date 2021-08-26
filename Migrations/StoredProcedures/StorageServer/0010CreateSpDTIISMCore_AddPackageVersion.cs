using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0010-CreateSpDTIISMCoreAddPackageVersion_StoredProcedure")]
    public class CreateSpDTIISMCore_AddPackageVersion : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Add new site to table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMCore_AddPackageVersion]
@SiteId INT,
@Name NVARCHAR(250),
@NewVersion NVARCHAR(100)
AS
BEGIN
    DECLARE @CNT INT
    SELECT 
        @CNT=COUNT(*) 
    FROM 
        dbo.IISSitePackageVersion 
    WHERE 
        SiteId = @SiteId 
            AND Name = @Name
    
    IF @CNT>0
        UPDATE 
            dbo.IISSitePackage
        SET  
            Name=@Name, NewVersion=@NewVersion,SiteId = @SiteId
        OUTPUT 
            INSERTED.[Id]
        WHERE 
            SiteId = @SiteId AND Name = @Name
    ELSE
        INSERT INTO 
            dbo.IISSitePackageVersion
            (SiteId,Name,NewVersion)
        OUTPUT 
            INSERTED.[Id]
        VALUES
            (@SiteId,@Name,@NewVersion)
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
