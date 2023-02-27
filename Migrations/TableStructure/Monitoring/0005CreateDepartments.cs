using DSM.UI.Api.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Monitoring
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("B0005-CreateDepartments_Table")]
    public class CreateDepartments : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "Departments", schema: "dbo", columns: table => new
            {
                DepartmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                DepartmentName = table.Column<string>(nullable: false, type: "varchar(100)"),
                DepartmentEmail = table.Column<string>(nullable: true, type: "varchar(100)"),
                DepartmentPhone = table.Column<string>(nullable: true, type: "varchar(20)"),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_DepartmentId", x => x.DepartmentId);
            });
        }
    }
}
