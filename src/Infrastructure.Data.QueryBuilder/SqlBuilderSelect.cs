using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder
{
    public abstract class SqlBuilderSelect : SqlBuilderCommand
    {
        protected SqlBuilderWhere where;

        public SqlBuilderSelect(IBuilderFactory builderFactory, string schemaName, string tableName) : base(builderFactory, schemaName, tableName)
        {
            this.where = Where();
        }

        public new SqlBuilderSelect AddColumns(params string[] columns)
        {
            base.AddColumns(columns);
            return this;
        }

        public new SqlBuilderSelect AddColumns<T>(T entity)
        {
            base.AddColumns<T>(entity);
            return this;
        }

        public new SqlBuilderSelect AddColumn(string columnName)
        {
            base.AddColumn(columnName);
            return this;
        }

        public new SqlBuilderSelect RenameColumn(string columnNameFrom, string columnNameTo)
        {
            base.RenameColumn(columnNameFrom, columnNameTo);
            return this;
        }

        public new SqlBuilderSelect RemoveColumns(params string[] columns)
        {
            base.RemoveColumns(columns);
            return this;
        }

        public new SqlBuilderSelect RemoveColumn(string columnName)
        {
            base.RemoveColumn(columnName);
            return this;
        }

        public SqlBuilderWhere Where()
        {
            this.where = builderFactory.CreateSqlBuilderWhere(schemaName, tableName, this);
            return where;
        }
    }
}
