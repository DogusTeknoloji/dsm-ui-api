using DSM.UI.Api.Enums;
using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.IIS
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("C0006-CreateIISSiteEndpoint_Table")]
    public class CreateIISSiteEndpoint : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "IISSiteEndpoint", schema: "dbo", columns: table => new
            {
                Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                DestinationServer = table.Column<string>(nullable: true, type: "varchar(256)"),
                DestinationAddress = table.Column<string>(nullable: true, type: "varchar(256)"),
                DestinationAddressType = table.Column<string>(nullable: true, type: "varchar(128)"),
                HostInformation = table.Column<string>(nullable: true, type: "varchar(1024)"),
                Port = table.Column<int>(nullable: false),
                EndpointUrl = table.Column<string>(nullable: true, type: "varchar(512)"),
                IsAvailable = table.Column<bool>(nullable: false),
                ServerResponse = table.Column<int>(nullable: false),
                ServerResponseDescription = table.Column<string>(nullable: true, type: "varchar(256)"),
                HttpProtocol = table.Column<string>(nullable: true, type: "varchar(128)"),
                LastCheckDate = table.Column<DateTime>(nullable: false, defaultValue: default(DateTime).DefaultSqlDateTime()),
                DeleteDate = table.Column<DateTime>(nullable: false, defaultValue: default(DateTime).DefaultSqlDateTime()),
                SendAlertMailWhenUnavailable = table.Column<bool>(nullable: false),
                SiteId = table.Column<long>(nullable: false),
                DeleteStatus = table.Column<bool>(nullable: true),
                ResponseTime = table.Column<long>(nullable: true),
                EndpointName = table.Column<string>(nullable: true, type: "varchar(256)"),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_IISSiteEndpoint_Id", x => x.Id);
                table.ForeignKey("FK_IISSiteEndpoint_SiteId", x => x.SiteId, principalTable: "IISSite", principalColumn: "Id");
            });
        }
    }
}
