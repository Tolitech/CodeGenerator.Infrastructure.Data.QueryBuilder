using System;
using Tolitech.CodeGenerator.Domain.Entities;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder
{
    public abstract class SqlBuilderInsert : SqlBuilderCommand
    {
        protected bool isIdentity;
        protected string? identityColumnName;

        public SqlBuilderInsert(IBuilderFactory builderFactory, string schemaName, string tableName) : base(builderFactory, schemaName, tableName)
        {
            this.isIdentity = false;
            this.identityColumnName = null;
        }

        public new SqlBuilderInsert AddColumns(params string[] columns)
        {
            base.AddColumns(columns);
            return this;
        }

        public new SqlBuilderInsert AddColumns<T>(T entity)
        {
            base.AddColumns<T>(entity);
            return this;
        }

        public new SqlBuilderInsert AddColumn(string columnName)
        {
            base.AddColumn(columnName);
            return this;
        }

        public new SqlBuilderInsert RenameColumn(string columnNameFrom, string columnNameTo)
        {
            base.RenameColumn(columnNameFrom, columnNameTo);
            return this;
        }

        public new SqlBuilderInsert RemoveColumns(params string[] columns)
        {
            base.RemoveColumns(columns);
            return this;
        }

        public new SqlBuilderInsert RemoveColumn(string columnName)
        {
            base.RemoveColumn(columnName);
            return this;
        }

        public SqlBuilderInsert Identity(string columnName)
        {
            this.isIdentity = true;
            this.identityColumnName = columnName;
            base.RemoveColumn(columnName);
            return this;
        }
    }
}
