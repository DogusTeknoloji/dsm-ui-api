using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0019-CreateSpDTIISMSHCAddNewConnectionStringMultiple_StoredProcedure")]
    public class CreateSpDTIISMSHCAddNewConnectionStringMultiple : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
 -- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Add new connection string to table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMSHC_AddNewConnectionStringMultiple]
    @Table SiteConnectionStringObj READONLY
AS
BEGIN
    MERGE dbo.IISSiteConnectionString AS Source USING @Table AS Dest
    ON (Source.SiteId = Dest.SiteId AND Source.ServerName =Dest.ServerName AND Source.DatabaseName = Dest.DatabaseName AND Source.ConnectionName =  Dest.ConnectionName)
    WHEN MATCHED THEN UPDATE
        SET 
            DatabaseName = Dest.DatabaseName, DeleteDate = Dest.DeleteDate, 
            IsAvailable = Dest.IsAvailable, LastCheckTime = Dest.LastCheckTime, 
            Password = Dest.Password, Port = Dest.Port,
            RawConnectionString =Dest.RawConnectionString, ServerName = Dest.ServerName, 
            SiteId =Dest.SiteId, UserName = Dest.UserName, ResponseTime = Dest.ResponseTime,
            SendAlertMailWhenUnavailable =  Dest.SendAlertMailWhenUnavailable,
            ConnectionName = Dest.ConnectionName
    WHEN NOT MATCHED BY TARGET THEN INSERT
            (DatabaseName,DeleteDate,IsAvailable,LastCheckTime,
             Password,Port,RawConnectionString,ServerName,SiteId,
             UserName,ResponseTime,SendAlertMailWhenUnavailable,
             ConnectionName)
        VALUES
            (Dest.DatabaseName,Dest.DeleteDate,Dest.IsAvailable,
             Dest.LastCheckTime,Dest.Password,Dest.Port,
             Dest.RawConnectionString,Dest.ServerName,Dest.SiteId,
             Dest.UserName,Dest.ResponseTime,Dest.SendAlertMailWhenUnavailable,
             Dest.ConnectionName);
        
        SELECT 
            * 
        FROM 
            dbo.IISSiteConnectionString 
        WHERE 
            SiteId IN (SELECT SiteId FROM @Table) 
                AND ServerName IN (SELECT ServerName FROM @Table)
                AND DatabaseName IN (SELECT DatabaseName FROM @Table) 
                AND ConnectionName IN (SELECT ConnectionName FROM @Table)
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
