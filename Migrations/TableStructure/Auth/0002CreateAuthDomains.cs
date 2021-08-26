using DSM.UI.Api.Enums;
using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Migrations.TableStructure.Auth
{
    [DbContext(typeof(DSMAuthDbContext))]
    [Migration("0002-CreateAuthDomains_Table")]
    public class CreateAuthDomains : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "AuthDomains", schema: "dbo", columns: table => new
            {
                AuthDomainId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                DomainName = table.Column<string>(nullable: false, type: "varchar(100)"),
                DomainAddress = table.Column<string>(nullable: false, type: "varchar(150)"),
                RowState = table.Column<short>(nullable: false, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_AuthDomainId", x => x.AuthDomainId);
            });
        }
    }
}
