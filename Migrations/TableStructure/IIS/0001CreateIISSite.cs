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
    [Migration("C0001-CreateIISSite_Table")]
    public class CreateIISSite : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "IISSite", schema: "dbo", columns: table => new
            {
                Id = table.Column<long>(nullable: false) // IISSiteId fix convention
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                MachineName = table.Column<string>(nullable: true, type: "varchar(200)"),
                Name = table.Column<string>(nullable: true, type: "varchar(200)"),
                ApplicationPoolName = table.Column<string>(nullable: true, type: "varchar(200)"),
                PhysicalPath = table.Column<string>(nullable: true, type: "varchar(512)"),
                EnabledProtocols = table.Column<string>(nullable: true, type: "varchar(150)"),
                MaxBandWitdh = table.Column<long>(nullable: false), //MaxBandwidth fix typo
                MaxConnections = table.Column<long>(nullable: false),
                LogFileEnabled = table.Column<bool>(nullable: false),
                LogFileDirectory = table.Column<string>(nullable: true, type: "varchar(512)"),
                LogFormat = table.Column<string>(nullable: true, type: "varchar(256)"),
                LogPeriod = table.Column<string>(nullable: true, type: "varchar(256)"),
                ServerAutoStart = table.Column<bool>(nullable: false),
                State = table.Column<string>(nullable: true, type: "varchar(256)"),
                TraceFailedRequestsLoggingEnabled = table.Column<bool>(nullable: false),
                TraceFailedRequestsLoggingDirectory = table.Column<string>(nullable: true, type: "varchar(512)"),
                LastUpdated = table.Column<DateTime>(nullable: true, defaultValue: default(DateTime).DefaultSqlDateTime()),
                DateDeleted = table.Column<DateTime>(nullable: true, defaultValue: default(DateTime).DefaultSqlDateTime()),
                IISSiteId = table.Column<long>(nullable: true), // SiteIdIIS rename field
                NetFrameworkVersion = table.Column<string>(nullable: true, type: "varchar(50)"),
                IsAvailable = table.Column<bool>(nullable: true),
                LastCheckTime = table.Column<DateTime>(nullable: true, defaultValue: default(DateTime).DefaultSqlDateTime()),
                SendAlertMailWhenUnavailable = table.Column<bool>(nullable: true),
                AppType = table.Column<string>(nullable: true, type: "varchar(30)"),
                WebConfigLastBackupDate = table.Column<DateTime>(nullable: true, defaultValue: default(DateTime).DefaultSqlDateTime()),
                WebConfigBackupDirectory = table.Column<string>(nullable: true, type: "varchar(200)"),
                RowState = table.Column<short>(nullable: true, defaultValue: RowState.Enabled),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_IISSite_Id", x => x.Id);
            });
        }
    }
}
