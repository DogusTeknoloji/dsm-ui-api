using DSM.UI.Api.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.IIS
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("C0005-CreateIISSiteWebConfiguration_Table")]
    public class CreateIISSiteWebConfiguration : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "IISSiteWebConfiguration", schema: "dbo", columns: table => new
            {
                Id = table.Column<long>(nullable: false),
                ContentRaw = table.Column<string>(nullable: true, type: "text"),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_IISSiteWebConfiguration_Id", x => x.Id);
                table.ForeignKey("FK_IISSiteWebConfiguration_Id", x => x.Id, principalTable: "IISSite", principalColumn: "Id");
            });
        }
    }
}
