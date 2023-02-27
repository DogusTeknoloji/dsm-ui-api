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
    [Migration("0005-CreateAuthUsers_Table")]
    public class CreateAuthUsers : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "AuthUsers", schema: "dbo", columns: table => new
            {
                AuthUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Username = table.Column<string>(nullable: false, type: "varchar(100)"),
                AuthDomainId = table.Column<int>(nullable: true),
                AuthKey = table.Column<string>(nullable: true, type: "varchar(512)"),
                FullName = table.Column<string>(nullable: false, type: "varchar(200)"),
                Password = table.Column<string>(nullable: false, type: "varchar(100)"),
                PasswordHash = table.Column<string>(nullable: false, type: "varchar(256)"),
                ProfileImage = table.Column<string>(nullable: true, type: "varchar(max)"),
                RowState = table.Column<short>(nullable: false, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_AuthUserId", x => x.AuthUserId);
                table.ForeignKey("FK_AuthUsers_DomainId", x => x.AuthDomainId, principalTable: "AuthDomains", principalColumn: "AuthDomainId");
            });
        }
    }
}
