using DSM.UI.Api.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.IIS
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("C0002-CreateIISSiteBinding_Table")]
    public class CreateIISSiteBinding : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "IISSiteBinding", schema: "dbo", columns: table => new
            {
                Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                IpAddress = table.Column<string>(nullable: true, type: "varchar(64)"),
                IpAdressFamily = table.Column<string>(nullable: true, type: "varchar(128)"), // IpAddress fix
                Port = table.Column<string>(nullable: true, type: "varchar(50)"),
                Host = table.Column<string>(nullable: true, type: "varchar(128)"),
                Protocol = table.Column<string>(nullable: true, type: "varchar(128)"),
                SiteId = table.Column<long>(nullable: false),
                IsSSLBound = table.Column<bool>(nullable: false),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_IISSiteBinding_Id", x => x.Id);
                table.ForeignKey("FK_IISSiteBinding_SiteId", x => x.SiteId, principalTable: "IISSite", principalColumn: "Id");
            });
        }
    }
}
