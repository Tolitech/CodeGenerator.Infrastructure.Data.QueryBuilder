using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder
{
    public abstract class SqlBuilder : ISqlBuilder
    {
        protected IBuilderFactory? _builderFactory;

        public SqlBuilderInsert Insert(string tableName)
        {
            return Insert("", tableName);
        }

        public SqlBuilderInsert Insert(string schemaName, string tableName)
        {
            if (_builderFactory == null)
                throw new InvalidOperationException();

            return _builderFactory.CreateSqlBuilderInsert(schemaName, tableName);
        }

        public SqlBuilderUpdate Update(string tableName)
        {
            return Update("", tableName);
        }

        public SqlBuilderUpdate Update(string schemaName, string tableName)
        {
            if (_builderFactory == null)
                throw new InvalidOperationException();

            return _builderFactory.CreateSqlBuilderUpdate(schemaName, tableName);
        }

        public SqlBuilderDelete Delete(string tableName)
        {
            return Delete("", tableName);
        }

        public SqlBuilderDelete Delete(string schemaName, string tableName)
        {
            if (_builderFactory == null)
                throw new InvalidOperationException();

            return _builderFactory.CreateSqlBuilderDelete(schemaName, tableName);
        }

        public SqlBuilderSelect Select(string tableName)
        {
            return Select("", tableName);
        }

        public SqlBuilderSelect Select(string schemaName, string tableName)
        {
            if (_builderFactory == null)
                throw new InvalidOperationException();

            return _builderFactory.CreateSqlBuilderSelect(schemaName, tableName);
        }

        protected void SetBuilderFactory(IBuilderFactory builderFactory)
        {
            _builderFactory = builderFactory;
        }
    }
}