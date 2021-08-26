using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0020-CreateSpDTIISMSHCAddNewEndpoint_StoredProcedure")]
    public class CreateSpDTIISMSHCAddNewEndpoint : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Adds new Endpoint to table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMSHC_AddNewEndpoint]
    @SiteId INT,
    @EndpointUrl NVARCHAR(200),
    @DeleteDate DATETIME,
    @DestinationAddress NVARCHAR(150),
    @DestinationAddressType NVARCHAR(100),
    @DestinationServer NVARCHAR(150),
    @HostInformation NVARCHAR(250),
    @HttpProtocol NVARCHAR(100),
    @IsAvailable BIT,
    @LastCheckDate DATETIME,
    @Port INT,
    @ServerResponse NVARCHAR(250),
    @ServerResponseDescription NVARCHAR(300),
    @SendAlertMailWhenUnavailable BIT
AS
BEGIN
    DECLARE @CNT INT
    
    SELECT 
        @CNT = COUNT(*) 
    FROM 
        dbo.IISSiteEndpoint
    WHERE 
        SiteId=  @SiteId 
            AND  EndpointUrl= @EndpointUrl
    
    IF @CNT > 0
        UPDATE 
            dbo.IISSiteEndpoint
        SET  
            DeleteDate = @DeleteDate, DestinationAddress=@DestinationAddress, DestinationAddressType=@DestinationAddressType, DestinationServer=@DestinationServer, EndpointUrl=@EndpointUrl,
            HostInformation =@HostInformation, HttpProtocol=@HttpProtocol, IsAvailable=@IsAvailable, LastCheckDate=@LastCheckDate, Port=@Port, 
            ServerResponse=@ServerResponse, ServerResponseDescription=@ServerResponseDescription, SiteId=@SiteId 
        OUTPUT 
            INSERTED.[Id]
        WHERE 
            SiteId = @SiteId 
                AND EndpointUrl= @EndpointUrl
    ELSE
        INSERT INTO 
            dbo.IISSiteEndpoint 
                ( DeleteDate, DestinationAddress, DestinationAddressType, DestinationServer, EndpointUrl, 
                  HostInformation, HttpProtocol, IsAvailable, LastCheckDate, Port, 
                  SendAlertMailWhenUnavailable, ServerResponse, ServerResponseDescription, SiteId)
         OUTPUT 
            INSERTED.[Id]
        VALUES ( @DeleteDate, @DestinationAddress, @DestinationAddressType, @DestinationServer, @EndpointUrl, 
                 @HostInformation, @HttpProtocol, @IsAvailable, @LastCheckDate, @Port, 
                 @SendAlertMailWhenUnavailable, @ServerResponse, @ServerResponseDescription, @SiteId)
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
