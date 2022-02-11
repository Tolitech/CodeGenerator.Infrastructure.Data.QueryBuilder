using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.Tests.SqlServer
{
    public class SqlBuilderUpdate : QueryBuilder.SqlBuilderUpdate
    {
        public SqlBuilderUpdate(IBuilderFactory builderFactory, string schemaName, string tableName) : base(builderFactory, schemaName, tableName)
        {

        }

        public override string Build()
        {
            string sql = $"update [{schemaName}].[{tableName}] set {GetSqlColumnsToUpdate()}{where.BuildWhere()};";
            return sql;
        }

        private string GetSqlColumnsToUpdate()
        {
            string sql = "";
            bool firstTime = true;

            foreach (var updateColumn in columns)
            {
                sql += (firstTime ? "" : ", ");
                sql += $"[{base.GetColumnName(updateColumn)}] = @{updateColumn}";
                firstTime = false;
            }

            return sql;
        }
    }
}
