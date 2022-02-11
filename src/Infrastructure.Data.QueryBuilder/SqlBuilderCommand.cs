using System;
using System.Reflection;
using Tolitech.CodeGenerator.Domain.Entities;
using Tolitech.CodeGenerator.Domain.ValueObjects;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder
{
    public abstract class SqlBuilderCommand : ISqlCommand
    {
        protected IBuilderFactory builderFactory;

        protected string schemaName;
        protected string tableName;

        protected IList<string> columns;
        protected IDictionary<string, string> columnsRenamed;
        protected IList<string> conditions;

        public SqlBuilderCommand(IBuilderFactory builderFactory, string schemaName, string tableName)
        {
            this.builderFactory = builderFactory;
            this.schemaName = schemaName;
            this.tableName = tableName;

            columns = new List<string>();
            columnsRenamed = new Dictionary<string, string>();
            conditions = new List<string>();
        }

        #region Add columns

        protected void AddColumns(params string[] columns)
        {
            foreach (var column in columns)
                this.columns.Add(column);
        }

        protected void AddColumns<T>(T entity)
        {
            if (entity == null)
                return;

            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (!IsBaseType(prop.PropertyType, typeof(Entity)))
                {
                    if (!IsCollection(prop.PropertyType))
                    {
                        if (IsBaseType(prop.PropertyType, typeof(ValueObject)))
                        {
                            var properties = GetPropertiesValueObject(prop);

                            foreach (var _prop in properties)
                                columns.Add(_prop.Name);
                        }
                        else
                            columns.Add(prop.Name);
                    }
                }
            }
        }

        protected void AddColumn(string columnName)
        {
            columns.Add(columnName);
        }

        #endregion

        #region Rename column

        protected void RenameColumn(string columnNameFrom, string columnNameTo)
        {
            if (columns.Contains(columnNameFrom))
            {
                if (!columnsRenamed.ContainsKey(columnNameFrom))
                    columnsRenamed.Add(columnNameFrom, columnNameTo);
            }
        }

        #endregion

        #region Remove columns

        protected void RemoveColumns(params string[] columns)
        {
            foreach (var column in columns)
                this.columns.Remove(column);
        }

        protected void RemoveColumn(string columnName)
        {
            columns.Remove(columnName);
        }

        #endregion

        #region Add conditions

        protected void AddCondition(string condition)
        {
            this.conditions.Add(condition);
        }

        #endregion

        #region Reflection

        protected IList<PropertyInfo> GetPropertiesValueObject(PropertyInfo property)
        {
            IList<PropertyInfo> properties = new List<PropertyInfo>();

            var props = property.PropertyType.GetProperties();
            foreach (var prop in props)
            {
                if (!IsBaseType(prop.PropertyType, typeof(ValueObject)))
                {
                    if (!IsCollection(prop.PropertyType))
                    {
                        properties.Add(prop);
                    }
                }
            }

            return properties;
        }

        /// <summary>
        /// Checks whether the type is a base type.
        /// </summary>
        /// <param name="typeToCheck">type to check</param>
        /// <param name="type">type</param>
        /// <returns>is (true) or (false)</returns>
        protected bool IsBaseType(Type? typeToCheck, Type type)
        {
            if (typeToCheck == null)
                return false;

            if (typeToCheck == type)
                return true;

            return IsBaseType(typeToCheck.GetTypeInfo().BaseType, type);
        }

        /// <summary>
        /// Checks whether the object type is a collection.
        /// </summary>
        /// <param name="typeToCheck">type to check</param>
        /// <returns>is collection (true) or (false)</returns>
        protected static bool IsCollection(Type typeToCheck)
        {
            var typeInfo = typeToCheck.GetTypeInfo();

            if (typeInfo.IsGenericType)
            {
                Type type = typeInfo.GetGenericTypeDefinition();

                if (type == typeof(IEnumerable<>) || type == typeof(ICollection<>) || type == typeof(IList<>))
                    return true;
            }

            return false;
        }

        #endregion

        protected string GetColumnName(string columnName)
        {
            if (this.columnsRenamed.ContainsKey(columnName))
                return this.columnsRenamed[columnName];

            return columnName;
        }

        abstract public string Build();
    }
}