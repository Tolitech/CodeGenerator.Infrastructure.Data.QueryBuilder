using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder
{
    public abstract class SqlBuilderWhere : SqlBuilderCommand
    {
        protected SqlBuilderCommand command;

        public SqlBuilderWhere(IBuilderFactory builderFactory, string schemaName, string tableName, SqlBuilderCommand command) : base(builderFactory, schemaName, tableName)
        {
            this.command = command;
        }

        public new SqlBuilderWhere AddColumns(params string[] columns)
        {
            base.AddColumns(columns);
            return this;
        }

        public new SqlBuilderWhere AddColumn(string columnName)
        {
            base.AddColumn(columnName);
            return this;
        }

        public new SqlBuilderWhere RenameColumn(string columnNameFrom, string columnNameTo)
        {
            base.RenameColumn(columnNameFrom, columnNameTo);
            return this;
        }

        public new SqlBuilderWhere RemoveColumns(params string[] columns)
        {
            base.RemoveColumns(columns);
            return this;
        }

        public new SqlBuilderWhere RemoveColumn(string columnName)
        {
            base.RemoveColumn(columnName);
            return this;
        }

        public new SqlBuilderWhere AddCondition(string condition)
        {
            base.AddCondition(condition);
            return this;
        }

        public override string Build()
        {
            string sql = $"{command.Build()}";
            return sql;
        }

        public abstract string BuildWhere();
    }
}
