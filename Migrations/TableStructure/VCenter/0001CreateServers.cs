using DSM.UI.Api.Enums;
using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.VCenter
{
    [DbContext(typeof(DSMVCenterDbContext))]
    [Migration("0002-CreateServers_Table")]
    public class CreateServers : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "Servers", schema: "dbo", columns: table => new
            {
                ServerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                ServerName = table.Column<string>(nullable: false, type: "varchar(256)"),
                HostName = table.Column<string>(nullable: false, type: "varchar(256)"),
                Company = table.Column<string>(nullable: true, type: "varchar(128)"),
                IpAddress = table.Column<string>(nullable: true, type: "varchar(128)"),
                CustomIp = table.Column<string>(nullable: true, type: "varchar(128)"),
                PhysicalLocation = table.Column<string>(nullable: true, type: "varchar(128)"),
                Responsible = table.Column<string>(nullable: true, type: "varchar(256)"),
                ServerType = table.Column<string>(nullable: true, type: "varchar(256)"),
                Service = table.Column<string>(nullable: true, type: "varchar(256)"),
                LastBackup = table.Column<DateTime>(nullable: true),
                OperatingSystem = table.Column<string>(nullable: true, type: "varchar(64)"),
                PowerState = table.Column<string>(nullable: true, type: "varchar(64)"),
                Boottime = table.Column<DateTime>(nullable: true),
                TotalCPU = table.Column<int>(nullable: true),
                TotalMemory = table.Column<int>(nullable: true),
                MemoryUsage = table.Column<int>(nullable: true),
                ToolsRunningStatus = table.Column<string>(nullable: true, type: "varchar(256)"),
                ESXI = table.Column<string>(nullable: true, type: "varchar(256)"),
                Cluster = table.Column<string>(nullable: true, type: "varchar(256)"),
                Notes = table.Column<string>(nullable: true, type: "varchar(1024)"),
                OdmReplication = table.Column<string>(nullable: true, type: "varchar(48)"),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled)
            }, constraints: table =>
            {
                table.PrimaryKey("PK_ServerId", x => x.ServerId);
            });
        }
    }
}
