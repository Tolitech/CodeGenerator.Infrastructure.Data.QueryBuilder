using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder
{
    public interface IBuilderFactory
    {
        SqlBuilderInsert CreateSqlBuilderInsert(string schemaName, string tableName);
        
        SqlBuilderUpdate CreateSqlBuilderUpdate(string schemaName, string tableName);
        
        SqlBuilderDelete CreateSqlBuilderDelete(string schemaName, string tableName);
        
        SqlBuilderSelect CreateSqlBuilderSelect(string schemaName, string tableName);

        SqlBuilderWhere CreateSqlBuilderWhere(string schemaName, string tableName, SqlBuilderCommand command);
    }
}