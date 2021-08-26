using DSM.UI.Api.Enums;
using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.IIS
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("C0003-CreateIISSiteConnectionString_Table")]
    public class CreateIISSiteConnectionString : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "IISSiteConnectionString", schema: "dbo", columns: table => new
            {
                Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                SiteId = table.Column<long>(nullable: false),
                RawConnectionString = table.Column<string>(nullable: true, type: "varchar(1024)"),
                ServerName = table.Column<string>(nullable: true, type: "varchar(256)"),
                Port = table.Column<int>(nullable: false),
                DatabaseName = table.Column<string>(nullable: true, type: "varchar(256)"),
                UserName = table.Column<string>(nullable: true, type: "varchar(256)"),
                Password = table.Column<string>(nullable: true, type: "varchar(256)"),
                IsAvailable = table.Column<bool>(nullable: false),
                LastCheckTime = table.Column<DateTime>(nullable: true, defaultValue: default(DateTime).DefaultSqlDateTime()),
                DeleteDate = table.Column<DateTime>(nullable: true, defaultValue: default(DateTime).DefaultSqlDateTime()),
                SendAlertMailWhenUnavailable = table.Column<bool>(nullable: false),
                ResponseTime = table.Column<long>(nullable: true),
                ConnectionName = table.Column<string>(nullable: true, type: "varchar(256)"),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_IISSiteConnectionString_Id", x => x.Id);
                table.ForeignKey("FK_IISSiteConnectionString_SiteId", x => x.SiteId, principalTable: "IISSite", principalColumn: "Id");
            });
        }
    }
}
