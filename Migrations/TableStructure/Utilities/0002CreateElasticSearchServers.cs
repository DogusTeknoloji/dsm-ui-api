using DSM.UI.Api.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Utilities
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("E0002-CreateElasticSearchServers_Table")]
    public class CreateElasticSearchServers : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "ElasticSearchServers", schema: "dbo", columns: table => new
            {
                ElasticSearchServerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Description = table.Column<string>(nullable: true, type: "varchar(512)"),
                Url = table.Column<string>(nullable: false, type: "varchar(512)"),
                Username = table.Column<string>(nullable: true, type: "varchar(128)"),
                Password = table.Column<string>(nullable: true, type: "varchar(128)"),
                Hostname = table.Column<string>(nullable: true, type: "varchar(128)"),
                IpAddress = table.Column<string>(nullable: true, type: "varchar(64)"),
                LoadBalancerIp = table.Column<string>(nullable: true, type: "varchar(64)"),
                CompanyId = table.Column<int>(nullable: false),
                ServerId = table.Column<int>(nullable: false),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_ElasticSearchServerId", x => x.ElasticSearchServerId);
                table.ForeignKey("FK_ElasticSearchServers_CompanyId", x => x.CompanyId, principalTable: "Companies", principalColumn: "CompanyId");
                table.ForeignKey("FK_ElasticSearchServers_ServerId", x => x.ServerId, principalTable: "Servers", principalColumn: "ServerId");
            });
        }
    }
}