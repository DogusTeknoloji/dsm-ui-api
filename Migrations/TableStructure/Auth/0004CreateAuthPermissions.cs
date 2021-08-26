using DSM.UI.Api.Enums;
using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Auth
{
    [DbContext(typeof(DSMAuthDbContext))]
    [Migration("0004-CreateAuthPermissions_Table")]
    public class CreateAuthPermissions : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "AuthPermissions", schema: "dbo", columns: table => new
            {
                AuthPermissionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                ControllerName = table.Column<string>(nullable: false, type: "varchar(100)"),
                ActionName = table.Column<string>(nullable: false, type: "varchar(100)"),
                RowState = table.Column<short>(nullable: false, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_AuthPermissionId", x => x.AuthPermissionId);
            });
        }
    }
}
