using DSM.UI.Api.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.IIS
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("C0004-CreateIISSitePackageVersion_Table")]
    public class CreateIISSitePackageVersion : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "IISSitePackageVersion", schema: "dbo", columns: table => new
            {
                Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(nullable: true, type: "varchar(256)"),
                NewVersion = table.Column<string>(nullable: true, type: "varchar(100)"),
                SiteId = table.Column<long>(nullable: false),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_IISSitePackageVersion_Id", x => x.Id);
                table.ForeignKey("FK_IISSitePackageVersion_SiteId", x => x.SiteId, principalTable: "IISSite", principalColumn: "Id");
            });
        }
    }
}
