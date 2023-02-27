using DSM.UI.Api.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Monitoring
{
    public class Alerts
    {
        [DbContext(typeof(DSMStorageDataContext))]
        [Migration("B0001-CreateAlerts_Table")]
        public class CreateCompany : Migration
        {
            protected override void Up([NotNull] MigrationBuilder migrationBuilder)
            {
                migrationBuilder.CreateTable(name: "Alerts", schema: "dbo", columns: table => new
                {
                    AlertId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlertDescription = table.Column<string>(nullable: false, type: "varchar(300)"),
                    Action1 = table.Column<string>(nullable: false, type: "varchar(300)"),
                    Action2 = table.Column<string>(nullable: true, type: "varchar(300)"),
                    Action3 = table.Column<string>(nullable: true, type: "varchar(300)"),
                    Action4 = table.Column<string>(nullable: true, type: "varchar(300)"),
                    AlertSource = table.Column<string>(nullable: false, type: "varchar(150)"),
                    Domain = table.Column<string>(nullable: true, type: "varchar(150)"),
                    RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
                }, constraints: table =>
                {
                    table.PrimaryKey("PK_AlertId", x => x.AlertId);
                });
            }
        }
    }
}
