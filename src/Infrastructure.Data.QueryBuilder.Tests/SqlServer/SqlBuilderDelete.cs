using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.Tests.SqlServer
{
    public class SqlBuilderDelete : QueryBuilder.SqlBuilderDelete
    {
        public SqlBuilderDelete(IBuilderFactory builderFactory, string schemaName, string tableName) : base(builderFactory, schemaName, tableName)
        {

        }

        public override string Build()
        {
            string sql = $"delete from [{schemaName}].[{tableName}]{where.BuildWhere()};";
            return sql;
        }
    }
}
