using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Migrations.Functions.StorageServer
{
    [DbContext(typeof(DSMStorageDataContext))]
    [Migration("ZX0001-CreateSQLToGenericType_Function")]
    public class CreateSQLToGenericType : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            string functionCreateQuery = @"
CREATE FUNCTION SQLToGenericType (@TypeName VARCHAR(20))
RETURNS VARCHAR(20)
AS
BEGIN
    DECLARE @RetVal VARCHAR(20)
    SELECT @RetVal = 
            CASE LOWER(@TypeName)
                WHEN 'bigint' THEN 'int64' 
                WHEN 'nvarchar' THEN 'string'
                WHEN 'varchar' THEN 'string'
                WHEN 'bit' THEN 'boolean'
                WHEN 'datetime2' THEN 'datetime' 
                WHEN 'datetime' THEN 'datetime'
                WHEN 'int' THEN 'int32'
                WHEN 'smallint' THEN 'int16'
                WHEN 'char' THEN 'string'
                WHEN 'money' THEN 'decimal'
                WHEN 'real' THEN 'decimal'
                WHEN 'float' THEN 'float'
                WHEN 'date' THEN 'datetime'
                ELSE @TypeName
            END
    RETURN (@RetVal)
END
";
            migrationBuilder.Sql(sql: functionCreateQuery);
        }
    }
}
