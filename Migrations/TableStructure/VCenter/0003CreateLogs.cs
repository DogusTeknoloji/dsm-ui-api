using DSM.UI.Api.Enums;
using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.VCenter
{
    [DbContext(typeof(DSMVCenterDbContext))]
    [Migration("0003-CreateLogs_Table")]
    public class CreateLogs : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "Logs", schema: "dbo", columns: table => new
            {
                LogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                LogName = table.Column<string>(nullable: false, type: "varchar(64)"),
                LogValue = table.Column<string>(nullable: true, type: "varchar(128)"),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled)
            }, constraints: table =>
            {
                table.PrimaryKey("PK_LogId", x => x.LogId);
            });
        }
    }
}
