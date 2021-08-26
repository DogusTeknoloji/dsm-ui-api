using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0004-CreateSpDTIISMCoreAddNewBindingMultiple_StoredProcedure")]
    public class CreateSpDTIISMCoreAddNewBindingMultiple : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author:		Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Add new site to table
-- =============================================
CREATE PROCEDURE SP_DTIISMCore_AddNewBindingMultiple
	@Table SiteBindingObj READONLY
AS
BEGIN
    MERGE dbo.IISSiteBinding AS SourceBinding USING @Table AS DestBinding
    ON (SourceBinding.Host = DestBinding.Host AND SourceBinding.Port = DestBinding.Port AND SourceBinding.SiteId = DestBinding.SiteId)
        WHEN MATCHED THEN UPDATE 
            SET IsSSLBound = DestBinding.IsSSLBound, IpAddress =  DestBinding.IpAddress,
                IpAdressFamily = DestBinding.IpAddressFamily, Port = DestBinding.Port, Host= DestBinding.Host, Protocol = DestBinding.Protocol,
                SiteId = DestBinding.SiteId
        WHEN NOT MATCHED BY TARGET THEN INSERT 
                (IsSSLBound, IpAddress, IpAdressFamily, Port, Host, Protocol,
                 SiteId)
            VALUES 
                (DestBinding.IsSSLBound, DestBinding.IpAddress, DestBinding.IpAddressFamily, DestBinding.Port, DestBinding.Host, DestBinding.Protocol,
                 DestBinding.SiteId);

    SELECT 
        Id, IsSSLBound, IpAddress, IpAdressFamily, Port, 
        Host, Protocol, SiteId 
    FROM 
        dbo.IISSiteBinding 
    WHERE 
        Host IN (SELECT Host FROM @Table) 
            AND Port IN (SELECT Port FROM @Table) 
            AND SiteId IN (SELECT SiteId FROM @Table)
    RETURN
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
