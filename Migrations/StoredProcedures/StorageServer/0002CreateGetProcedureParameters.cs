using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.StoredProcedures.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZY0002-CreateGetProcedureParameters_StoredProcedure")]
    public class CreateGetProcedureParameters : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string spCreateQuery = @"
CREATE PROCEDURE GetProcedureParameters
(
    @ProcedureName VARCHAR(120)
)
AS
BEGIN
    SELECT
        SUBSTRING(Parameters.name,2,LEN(Parameters.name)-1) AS ParamName,
        dbo.SQLToGenericType(Types.Name) AS TypeName,
        Parameters.max_length AS [MaxLength],
        0 AS Nullable
    FROM
        sys.parameters AS Parameters
            INNER JOIN sys.types AS Types
                ON Parameters.user_type_id = Types.user_type_id
    WHERE
        Parameters.object_id = OBJECT_ID(@ProcedureName)
END
";
            migrationBuilder.Sql(sql: spCreateQuery);
        }
    }
}
