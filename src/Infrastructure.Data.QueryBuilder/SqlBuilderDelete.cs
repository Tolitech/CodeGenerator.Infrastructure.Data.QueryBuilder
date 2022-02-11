using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder
{
    public abstract class SqlBuilderDelete : SqlBuilderCommand
    {
        protected SqlBuilderWhere where;

        public SqlBuilderDelete(IBuilderFactory builderFactory, string schemaName, string tableName) : base(builderFactory, schemaName, tableName)
        {
            this.where = Where();
        }

        public SqlBuilderWhere Where()
        {
            this.where = builderFactory.CreateSqlBuilderWhere(schemaName, tableName, this);
            return where;
        }
    }
}
