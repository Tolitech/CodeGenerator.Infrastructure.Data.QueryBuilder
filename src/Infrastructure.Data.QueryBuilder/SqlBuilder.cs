using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder
{
    public class SqlBuilder : ISqlBuilder
    {
        private static readonly Dictionary<string, IBuilderFactory> _builders;

        private readonly string _providerKey;

        static SqlBuilder()
        {
            _builders = new Dictionary<string, IBuilderFactory>();
        }

        public SqlBuilder()
        {
            _providerKey = "default";
        }

        public SqlBuilder(string providerKey)
        {
            _providerKey = providerKey;
        }

        public static void AddQueryBuilder(string key, IBuilderFactory builderFactory)
        {
            if (!_builders.ContainsKey(key))
                _builders.Add(key, builderFactory);
        }

        private IBuilderFactory GetBuilderFactory()
        {
            IBuilderFactory? builderFactory = null;
            _builders.TryGetValue(_providerKey, out builderFactory);

            if (builderFactory == null)
                throw new InvalidOperationException("No provider found for this key.");

            return builderFactory;
        }

        public SqlBuilderInsert Insert(string tableName)
        {
            return Insert("", tableName);
        }

        public SqlBuilderInsert Insert(string schemaName, string tableName)
        {
            var builderFactory = GetBuilderFactory();
            return builderFactory.CreateSqlBuilderInsert(schemaName, tableName);
        }

        public SqlBuilderUpdate Update(string tableName)
        {
            return Update("", tableName);
        }

        public SqlBuilderUpdate Update(string schemaName, string tableName)
        {
            var builderFactory = GetBuilderFactory();
            return builderFactory.CreateSqlBuilderUpdate(schemaName, tableName);
        }

        public SqlBuilderDelete Delete(string tableName)
        {
            return Delete("", tableName);
        }

        public SqlBuilderDelete Delete(string schemaName, string tableName)
        {
            var builderFactory = GetBuilderFactory();
            return builderFactory.CreateSqlBuilderDelete(schemaName, tableName);
        }

        public SqlBuilderSelect Select(string tableName)
        {
            return Select("", tableName);
        }

        public SqlBuilderSelect Select(string schemaName, string tableName)
        {
            var builderFactory = GetBuilderFactory();
            return builderFactory.CreateSqlBuilderSelect(schemaName, tableName);
        }
    }
}