using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0009-CreateSpDTIISMCoreAddPackageInfoMultiple_StoredProcedure")]
    public class CreateSpDTIISMCoreAddPackageInfoMultiple : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
-- =============================================
-- Author: Onur Akkaya
-- Create date: 19.03.2019
-- Description:	Add new site to table
-- =============================================
CREATE PROCEDURE [dbo].[SP_DTIISMCore_AddPackageInfoMultiple]
@Table SitePackageObj READONLY
AS
BEGIN
    MERGE dbo.IISSitePackageVersion AS SourcePackage USING @Table AS DestPackage
        ON (SourcePackage.SiteId = DestPackage.SiteId AND SourcePackage.Name = DestPackage.Name)
            WHEN MATCHED THEN UPDATE
                SET 
                    Name = DestPackage.Name, NewVersion = DestPackage.NewVersion, SiteId = DestPackage.SiteId
            WHEN NOT MATCHED BY TARGET THEN INSERT
                    (Name,NewVersion,SiteId)
                VALUES (DestPackage.Name, DestPackage.NewVersion, DestPackage.SiteId);
 
    SELECT 
        * 
    FROM 
        dbo.IISSitePackageVersion 
    WHERE 
        Name IN (SELECT Name FROM @Table) 
            AND SiteId IN (SELECT TOP 1 SiteId  FROM @Table)
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
