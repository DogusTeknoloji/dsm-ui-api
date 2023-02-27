using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.Types.TableTypes.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZW0003-CreateSiteEndpointObj_Type")]
    public class CreateSiteEndpointObj : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string typeCreateQuery = @"
CREATE TYPE SiteEndpointObj AS TABLE
(
    [DestinationServer] [varchar](150) NULL,
    [DestinationAddress] [varchar](150) NULL,
    [DestinationAddressType] [varchar](100) NULL,
    [HostInformation] [varchar](250) NULL,
    [Port] [int] NULL,
    [EndpointUrl] [varchar](200) NULL,
    [IsAvailable] [bit] NULL,
    [ServerResponse] [varchar](250) NULL,
    [ServerResponseDescription] [varchar](300) NULL,
    [HttpProtocol] [varchar](100) NULL,
    [LastCheckDate] [datetime] NULL,
    [DeleteDate] [datetime] NULL,
    [DeleteStatus] [bit] NULL,
    [SendAlertMailWhenUnavailable] [bit] NULL,
    [SiteId] [bigint] NULL,
    [ResponseTime] [bigint] NULL,
    [EndpointName] [varchar](256) NULL
)
";
            migrationBuilder.Sql(sql: typeCreateQuery);
        }
    }
}
