using DSM.UI.Api.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Monitoring
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("B0003-CreateExtendedContacts_Table")]
    public class CreateCompany : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "ExtendedContacts", schema: "dbo", columns: table => new
            {
                ExtendedContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                ContactId = table.Column<int>(nullable: true),
                FullName = table.Column<string>(nullable: false, type: "varchar(100)"),
                EMail = table.Column<string>(nullable: false, type: "varchar(50)"),
                Phone1 = table.Column<string>(nullable: true, type: "varchar(20)"),
                Phone2 = table.Column<string>(nullable: true, type: "varchar(20)"),
                Department = table.Column<string>(nullable: false, type: "varchar(90)"),
                Unit = table.Column<string>(nullable: false, type: "varchar(90)"),
                ManagerContactId = table.Column<int>(nullable: true),
                Notes = table.Column<string>(nullable: true, type: "varchar(300)"),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_AlertId", x => x.ExtendedContactId);
                table.ForeignKey("FK_ExtendedContacts_ContactId", x => x.ContactId, principalTable: "Contacts", principalColumn: "ContactId");
                table.ForeignKey("FK_ExtendedContacts_ManagerContactId", x => x.ManagerContactId, principalTable: "Contacts", principalColumn: "ContactId");
            });
        }
    }
}
