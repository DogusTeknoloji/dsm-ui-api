using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0021-CreateSpDTIISMSHCAddNewEndpointMultiple_StoredProcedure")]
    public class CreateSpDTIISMSHCAddNewEndpointMultiple : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Adds new Endpoint to table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMSHC_AddNewEndpointMultiple]
   @Table SiteEndpointObj READONLY
AS
BEGIN
    MERGE dbo.IISSiteEndpoint AS Source USING @Table AS Dest
    ON (Source.SiteId = Dest.SiteId AND Source.EndpointUrl = Dest.EndpointUrl AND Source.EndpointName = Dest.EndpointName)
    WHEN MATCHED THEN UPDATE
        SET 
            DeleteDate = Dest.DeleteDate, DestinationAddress = Dest.DestinationAddress,
            DestinationAddressType = Dest.DestinationAddressType, 
            DestinationServer = Dest.DestinationServer, EndpointUrl = Dest.EndpointUrl,
            HostInformation = Dest.HostInformation, HttpProtocol = Dest.HttpProtocol,
            IsAvailable = Dest.IsAvailable, LastCheckDate = Dest.LastCheckDate,
            Port = Dest.Port, ServerResponse = Dest.ServerResponse, 
            ServerResponseDescription = Dest.ServerResponseDescription, SiteId= Dest.SiteId, 
            DeleteStatus = Dest.DeleteStatus, ResponseTime = Dest.ResponseTime,
            SendAlertMailWhenUnavailable =  Dest.SendAlertMailWhenUnavailable, EndpointName = Dest.EndpointName
    WHEN NOT MATCHED BY TARGET THEN INSERT
        (DeleteDate, DestinationAddress,DestinationAddressType,DestinationServer,
         EndpointUrl,HostInformation,HttpProtocol,IsAvailable,LastCheckDate,Port,
         ServerResponse,ServerResponseDescription, SiteId,DeleteStatus,ResponseTime,
         SendAlertMailWhenUnavailable,EndpointName)
    VALUES 
        (Dest.DeleteDate,Dest.DestinationAddress,Dest.DestinationAddressType,
         Dest.DestinationServer,Dest.EndpointUrl,Dest.HostInformation,Dest.HttpProtocol,
         Dest.IsAvailable,Dest.LastCheckDate,Dest.Port,Dest.ServerResponse,
         Dest.ServerResponseDescription,Dest.SiteId,Dest.DeleteStatus,Dest.ResponseTime,
         Dest.SendAlertMailWhenUnavailable,Dest.EndpointName);
        
        SELECT 
            * 
        FROM 
            dbo.IISSiteEndpoint
        WHERE 
             SiteId IN (SELECT SiteId FROM @Table) 
                AND EndpointUrl IN (SELECT EndpointUrl FROM @Table) 
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
