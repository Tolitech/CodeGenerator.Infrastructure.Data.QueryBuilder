using System;
using System.Collections.Generic;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.Tests.SqlServer
{
    public class SqlBuilderInsert : QueryBuilder.SqlBuilderInsert
    {
        public SqlBuilderInsert(IBuilderFactory builderFactory, string schemaName, string tableName) : base(builderFactory, schemaName, tableName)
        {

        }

        public override string Build()
        {
            string sql = $"insert into [{schemaName}].[{tableName}] {GetSqlColumnsToInsert()};";

            if (isIdentity)
                sql += " select cast(SCOPE_IDENTITY() as int);";

            return sql;
        }

        private string GetSqlColumnsToInsert()
        {
            IList<string> columnsRenamed = new List<string>();
            foreach (var column in base.columns)
                columnsRenamed.Add($"[{base.GetColumnName(column)}]");

            string sql = "(" + string.Join(", ", columnsRenamed) + ")";
            sql += " values ";
            sql += "(@" + string.Join(", @", base.columns) + ")";
            return sql;
        }
    }
}
