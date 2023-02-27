using DSM.UI.Api.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Shared
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("A0002-CreateWebAccessLogs_Table")]
    public class CreateWebAccessLogs : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "WebAccessLogs", schema: "dbo", columns: table => new
            {
                WebAccessLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Username = table.Column<string>(nullable: true, type: "varchar(108)"),
                UserRole = table.Column<string>(nullable: true, type: "varchar(100)"),
                CredentialsIssuer = table.Column<string>(nullable: true, type: "varchar(100)"),
                CredetialsRemainingTime = table.Column<DateTime>(nullable: true),
                LogTimestamp = table.Column<DateTime>(nullable: false),
                RequestMethod = table.Column<string>(nullable: false, type: "varchar(16)"),
                ServerResponseCode = table.Column<int>(nullable: false),
                QueryString = table.Column<string>(nullable: true, type: "varchar(256)"),
                IsHttps = table.Column<bool>(nullable: false),
                Protocol = table.Column<string>(nullable: true, type: "varchar(64)"),
                UserIpAddress = table.Column<string>(nullable: true, type: "varchar(64)"),
                UserPort = table.Column<int>(nullable: true),
                DestinationIpAddress = table.Column<string>(nullable: true, type: "varchar(64)"),
                DestinationPort = table.Column<int>(nullable: true),
                UserBrowser = table.Column<string>(nullable: true, type: "varchar(128)"),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_WebAccessLogId", x => x.WebAccessLogId);
            });
        }
    }
}
