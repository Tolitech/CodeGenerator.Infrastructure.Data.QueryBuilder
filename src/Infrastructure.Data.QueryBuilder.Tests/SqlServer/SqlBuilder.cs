using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.Tests.SqlServer
{
    public class SqlBuilder : QueryBuilder.SqlBuilder
    {
        public SqlBuilder()
        {
            SetBuilderFactory(new SqlServerBuilderFactory());
        }
    }
}
