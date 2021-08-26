using DSM.UI.Api.Enums;
using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Auth
{
    [DbContext(typeof(DSMAuthDbContext))]
    [Migration("0001-CreateAuthGroups_Table")]
    public class CreateAuthGroups : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "AuthGroups", schema: "dbo", columns: table => new
            {
                AuthGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                AuthGroupName = table.Column<string>(nullable: false, type: "varchar(100)"),

                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
                Priority = table.Column<int>(nullable: false, type: "int", defaultValueSql: "3")
            }, constraints: table =>
            {
                table.PrimaryKey("PK_AuthGroupId", x => x.AuthGroupId);
            });
        }
    }
}
