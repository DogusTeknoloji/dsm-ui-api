using DSM.UI.Api.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Monitoring
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("B0002-CreateAlerts_Table")]
    public class CreateContacts : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "Contacts", schema: "dbo", columns: table => new
            {
                ContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                FullName = table.Column<string>(nullable: false, type: "varchar(100)"),
                EMail = table.Column<string>(nullable: false, type: "varchar(50)"),
                Phone1 = table.Column<string>(nullable: true, type: "varchar(20)"),
                Phone2 = table.Column<string>(nullable: true, type: "varchar(20)"),
                Department = table.Column<string>(nullable: false, type: "varchar(90)"),
                Unit = table.Column<string>(nullable: false, type: "varchar(90)"),
                ManagerContactId = table.Column<int>(nullable: true),
                Notes = table.Column<string>(nullable: true, type: "varchar(300)"),
                CompanyId = table.Column<int>(nullable: false),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_ContactId", x => x.ContactId);
                table.ForeignKey("FK_Contacts_ManagerContactId", x => x.ManagerContactId, principalTable: "Contacts", principalColumn: "ContactId");
            });
        }
    }
}
