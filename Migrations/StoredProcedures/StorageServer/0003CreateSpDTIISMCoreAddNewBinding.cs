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
    [Migration("ZY0003-CreateSpDTIISMCoreAddNewBinding_StoredProcedure")]
    public class CreateSpDTIISMCoreAddNewBinding : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author:		Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Adds new binding to table
-- =============================================
CREATE PROCEDURE SP_DTIISMCore_AddNewBinding
    @BindingInformation NVARCHAR(200),
    @Host NVARCHAR(150),
    @SiteId INT,
    @IsSSLBound BIT,
    @IpAddress NVARCHAR(64),
    @IpAddressFamily NVARCHAR(50),
    @Port INT,
    @Protocol NVARCHAR(40)
AS
BEGIN
    DECLARE @CNT INT
    SELECT 
        @CNT = COUNT(*) 
    FROM 
        dbo.IISSiteBinding
    WHERE 
        Host= @Host 
            AND SiteId = @SiteId 
            AND Port = @Port
    
    IF @CNT > 0
        UPDATE 
            dbo.IISSiteBinding 
        SET 
            IsSSLBound = @IsSSLBound, IpAddress = @IpAddress, IpAdressFamily = @IpAddressFamily, Port = @Port, Host = @Host, Protocol = @Protocol, 
            SiteId= @SiteId
        OUTPUT 
            INSERTED.[Id]
        WHERE  
            Host= @Host 
                AND SiteId =  @SiteId 
                AND Port =@Port
    ELSE
        INSERT INTO dbo.IISSiteBinding 
            ( IsSSLBound, IpAddress, IpAdressFamily, Port, Host, Protocol, 
              SiteId)
        OUTPUT 
            INSERTED.[Id]
        VALUES( @IsSSLBound, @IpAddress, @IpAddressFamily, @Port, @Host, @Protocol, 
                @SiteId)
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
