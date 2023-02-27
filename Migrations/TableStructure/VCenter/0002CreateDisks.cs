using DSM.UI.Api.Enums;
using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.VCenter
{
    [DbContext(typeof(DSMVCenterDbContext))]
    [Migration("0002-CreateDisks_Table")]
    public class CreateDisks : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "Disks", schema: "dbo", columns: table => new
            {
                DiskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                ServerId = table.Column<int>(nullable: false),
                DiskName = table.Column<string>(nullable: false, type: "varchar(255)"),
                DiskCapacity = table.Column<int>(nullable: false),
                DiskFreeSpace = table.Column<int>(nullable: false),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled)
            }, constraints: table =>
            {
                table.PrimaryKey("PK_DiskId", x => x.DiskId);
                table.ForeignKey("FK_Disks_ServerId", x => x.ServerId, principalTable: "Servers", principalColumn: "ServerId");
            });
        }
    }
}