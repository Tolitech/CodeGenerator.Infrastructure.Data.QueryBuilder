using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder
{
    public abstract class SqlBuilderUpdate : SqlBuilderCommand
    {
        protected SqlBuilderWhere where;

        public SqlBuilderUpdate(IBuilderFactory builderFactory, string schemaName, string tableName) : base(builderFactory, schemaName, tableName)
        {
            where = Where();
        }

        public new SqlBuilderUpdate AddColumns(params string[] columns)
        {
            base.AddColumns(columns);
            return this;
        }

        public new SqlBuilderUpdate AddColumns<T>(T entity)
        {
            base.AddColumns<T>(entity);
            return this;
        }

        public new SqlBuilderUpdate AddColumn(string columnName)
        {
            base.AddColumn(columnName);
            return this;
        }

        public new SqlBuilderUpdate RenameColumn(string columnNameFrom, string columnNameTo)
        {
            base.RenameColumn(columnNameFrom, columnNameTo);
            return this;
        }

        public new SqlBuilderUpdate RemoveColumns(params string[] columns)
        {
            base.RemoveColumns(columns);
            return this;
        }

        public new SqlBuilderUpdate RemoveColumn(string columnName)
        {
            base.RemoveColumn(columnName);
            return this;
        }

        public SqlBuilderWhere Where()
        {
            where = builderFactory.CreateSqlBuilderWhere(schemaName, tableName, this);
            return where;
        }
    }
}
