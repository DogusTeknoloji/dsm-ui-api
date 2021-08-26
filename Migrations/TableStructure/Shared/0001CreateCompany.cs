using DSM.UI.Api.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Shared
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("A0001-CreateCompany_Table")]
    public class CreateCompany : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "Companies", schema: "dbo", columns: table => new
            {
                CompanyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(nullable: false, type: "varchar(100)"),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_CompanyId", x => x.CompanyId);
            });
        }
    }
}
