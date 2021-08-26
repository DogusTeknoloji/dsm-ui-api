using DSM.UI.Api.Enums;
using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.TableStructure.Auth
{
    [DbContext(typeof(DSMAuthDbContext))]
    [Migration("0007-CreateAuthUserFeatures_Table")]
    public class CreateAuthUserFeatures : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "AuthUserFeatures", schema: "dbo", columns: table => new
            {
                AuthUserFeatureId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                AuthUserId = table.Column<int>(nullable: false),
                AuthFeatureId = table.Column<int>(nullable: false),
                AuthFeatureValue = table.Column<int>(nullable: false),
                RowState = table.Column<short>(nullable: false, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_AuthUserFeatureId", x => x.AuthUserFeatureId);
                table.ForeignKey("FK_AUF_AuthUserId", x => x.AuthUserId, principalTable: "AuthUsers", principalColumn: "AuthUserId");
                table.ForeignKey("FK_AUF_AuthFeatureId", x => x.AuthFeatureId, principalTable: "AuthFeatures", principalColumn: "AuthFeatureId");
            });
        }
    }
}
