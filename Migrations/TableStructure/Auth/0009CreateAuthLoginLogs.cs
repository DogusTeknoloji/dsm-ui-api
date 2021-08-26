using DSM.UI.Api.Enums;
using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Auth
{
    [DbContext(typeof(DSMAuthDbContext))]
    [Migration("0009-CreateAuthUsers_Table")]
    public class CreateAuthLoginLogs : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "AuthLoginLogs", schema: "dbo", columns: table => new
            {
                AuthLoginLogId = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                AuthUserId = table.Column<int>(nullable: false),
                AttemptDate = table.Column<DateTime>(nullable: false),
                IsLoginSuccessful = table.Column<bool>(nullable: false),
                RowState = table.Column<short>(nullable: false, defaultValue: RowState.Enabled)
            }, constraints: table =>
            {
                table.PrimaryKey("PK_AuthLoginLogId", x => x.AuthLoginLogId);
                table.ForeignKey("FK_AuthLoginLogs_AuthUserId", x => x.AuthUserId, principalTable: "AuthUsers", principalColumn: "AuthUserId");
            });
        }
    }
}
