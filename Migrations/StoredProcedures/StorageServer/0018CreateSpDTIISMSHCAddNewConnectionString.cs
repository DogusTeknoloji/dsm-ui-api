using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0018-CreateSpDTIISMSHCAddNewConnectionString_StoredProcedure")]
    public class CreateSpDTIISMSHCAddNewConnectionString : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Add new connection string to table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMSHC_AddNewConnectionString]
    @SiteId INT,
    @ServerName NVARCHAR(200),
    @DatabaseName NVARCHAR(200),
    @DeleteDate DATETIME,
    @IsAvailable BIT,
    @LastCheckTime DATETIME,
    @Password NVARCHAR(200),
    @Port INT,
    @RawConnectionString NVARCHAR(512),
    @UserName NVARCHAR(200),
    @SendAlertMailWhenUnavailable BIT
AS
BEGIN
    DECLARE @CNT INT
    SELECT 
        @CNT = COUNT(*) 
    FROM 
         dbo.IISSiteConnectionString
    WHERE 
         SiteId = @SiteId 
             AND  ServerName= @ServerName 
             AND DatabaseName =  @DatabaseName
    
    IF @CNT > 0
        UPDATE 
            dbo.IISSiteConnectionString 
        SET 
            DatabaseName = @DatabaseName, DeleteDate = @DeleteDate, IsAvailable = @IsAvailable, LastCheckTime= @LastCheckTime, Password= @Password,
            Port= @Port, RawConnectionString= @RawConnectionString, ServerName= @ServerName, SiteId= @SiteId, UserName= @UserName
        OUTPUT 
            INSERTED.[Id]
        WHERE 
            SiteId = @SiteId 
                AND ServerName= @ServerName 
                AND DatabaseName = @DatabaseName
        ELSE
            INSERT INTO dbo.IISSiteConnectionString 
                (DatabaseName, DeleteDate, IsAvailable, LastCheckTime, Password, 
                 Port, RawConnectionString, ServerName, SiteId, UserName, 
                 SendAlertMailWhenUnavailable)
            OUTPUT 
                INSERTED.[Id]
            VALUES (@DatabaseName, @DeleteDate, @IsAvailable, @LastCheckTime, @Password,
                    @Port, @RawConnectionString, @ServerName, @SiteId, @UserName, 
                    @SendAlertMailWhenUnavailable)
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
