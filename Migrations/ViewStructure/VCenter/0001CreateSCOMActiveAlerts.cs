using DSM.UI.Api.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.ViewStructure.VCenter
{
    [DbContext(typeof(DSMVCenterDbContext))]
    [Migration("ZZ0001-CreateSCOMActiveAlerts_View")]
    public class CreateSCOMActiveAlerts : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string viewCreateQuery = @"
CREATE VIEW SCOMActiveAlerts
AS
SELECT 
    NULLIF(
            CASE WHEN CHARINDEX('.', ServerName, 0) > -1 THEN
                SUBSTRING(ServerName, 0, CHARINDEX('.', ServerName, 0))
            ELSE
                ServerName END, '') AS ServerName,
    CASE IsMonitorAlert
        WHEN 1 THEN 'Yes'
        WHEN 0 THEN 'No' 
    END AS IsMonitorAlert,
    CASE [Priority]
        WHEN 2 THEN 'High'
        WHEN 1 THEN 'Medium'
        WHEN 0 THEN 'Low' 
    END AS [Priority],
    CASE Severity
        WHEN 2 THEN 'Critical'
        WHEN 1 THEN 'Warning'
        WHEN 0 THEN 'Information'
    END AS Severity,
    Category, TimeRaised, AlertStringDescription, AlertParams, 'New' AS ResolutionState
FROM
    (SELECT
        MonitoringObjectName AS ServerName, IsMonitorAlert, ResolutionState, [Priority], Severity, 
        Category, TimeRaised, RepeatCount, LanguageCode, AlertStringDescription, AlertParams
    FROM
        DTEKSCOMOPSDB1.operationsManager.dbo.AlertView AS AlertView WITH (NOLOCK)
    WHERE
        MonitoringObjectPath IS NULL 
            AND MonitoringObjectName IS NOT NULL
    UNION SELECT
        MonitoringObjectPath AS ServerName, IsMonitorAlert, ResolutionState, [Priority], Severity, 
        Category, TimeRaised, RepeatCount, LanguageCode, AlertStringDescription, AlertParams
    FROM
        DTEKSCOMOPSDB1.operationsManager.dbo.AlertView AS AlertView WITH (NOLOCK)
    WHERE
        MonitoringObjectName IS NULL 
            AND MonitoringObjectPath IS NOT NULL
    UNION SELECT
        MonitoringObjectPath AS ServerName, IsMonitorAlert, ResolutionState, [Priority], Severity, 
        Category, TimeRaised, RepeatCount, LanguageCode, AlertStringDescription, AlertParams
    FROM
        DTEKSCOMOPSDB1.operationsManager.dbo.AlertView AS AlertView WITH (NOLOCK)
    WHERE
        MonitoringObjectName IS NOT NULL 
            AND MonitoringObjectPath IS NOT NULL
    ) AS AlertView
WHERE
    AlertView.ResolutionState = 0
        AND AlertView.LanguageCode = 'ENU'
";
            migrationBuilder.Sql(sql: viewCreateQuery);
        }
    }
}
