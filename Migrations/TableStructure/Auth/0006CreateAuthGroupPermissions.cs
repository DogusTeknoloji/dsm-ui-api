using DSM.UI.Api.Enums;
using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Auth
{
    [DbContext(typeof(DSMAuthDbContext))]
    [Migration("0006-CreateAuthGroupPermissions_Table")]
    public class CreateAuthGroupPermissions : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "AuthGroupPermissions", schema: "dbo", columns: table => new
            {
                AuthGroupPermissionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                AuthGroupId = table.Column<int>(nullable: false),
                AuthPermissionId = table.Column<int>(nullable: false),
                RowState = table.Column<short>(nullable: false, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_AuthGroupPermissionId", x => x.AuthGroupPermissionId);
                table.ForeignKey("FK_AGP_AuthGroupId", x => x.AuthGroupId, principalTable: "AuthGroups", principalColumn: "AuthGroupId");
                table.ForeignKey("FK_AGP_AuthPermissionId", x => x.AuthPermissionId, principalTable: "AuthPermissions", principalColumn: "AuthPermissionId");
            });
        }
    }
}
