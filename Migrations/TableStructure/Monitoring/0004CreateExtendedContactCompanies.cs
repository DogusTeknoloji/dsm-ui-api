using DSM.UI.Api.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Monitoring
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("B0003-CreateExtendedContactCompanies_Table")]
    public class CreateExtendedContactCompanies : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "ExtendedContactCompanies", schema: "dbo", columns: table => new
            {
                ExtendedContactCompanyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                ExtendedContactId = table.Column<int>(nullable: false),
                CompanyId = table.Column<int>(nullable: false),
                Notes = table.Column<string>(nullable: true, type: "varchar(300)"),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_ExtendedContactCompanyId", x => x.ExtendedContactCompanyId);
                table.ForeignKey("FK_ExtendedContacts_ExtendedContactId", x => x.ExtendedContactId, principalTable: "ExtendedContacts", principalColumn: "ExtendedContactId");
                table.ForeignKey("FK_ExtendedContacts_CompanyId", x => x.CompanyId, principalTable: "Companies", principalColumn: "CompanyId");
            });
        }
    }
}
