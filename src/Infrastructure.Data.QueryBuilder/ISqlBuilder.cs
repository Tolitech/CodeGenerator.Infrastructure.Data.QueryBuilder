using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder
{
    public interface ISqlBuilder
    {
        SqlBuilderInsert Insert(string tableName);

        SqlBuilderInsert Insert(string schemaName, string tableName);

        SqlBuilderUpdate Update(string tableName);

        SqlBuilderUpdate Update(string schemaName, string tableName);

        SqlBuilderDelete Delete(string tableName);

        SqlBuilderDelete Delete(string schemaName, string tableName);

        SqlBuilderSelect Select(string tableName);

        SqlBuilderSelect Select(string schemaName, string tableName);
    }
}
