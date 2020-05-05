using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DSM.UI.Api.Helpers
{
    public static class EntityQueryable
    {

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering)
        {
            return OrderBy(source, ordering, true);
        }
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, bool ascending)
        {
            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            PropertyInfo property;
            Expression propertyAccess;
            if (ordering.Contains('.'))
            {
                // support to be sorted on child fields.
                String[] childProperties = ordering.Split('.');
                property = type.GetProperty(childProperties[0]);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
                for (int i = 1; i < childProperties.Length; i++)
                {
                    property = property.PropertyType.GetProperty(childProperties[i]);
                    propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                }
            }
            else
            {
                property = typeof(T).GetProperty(ordering);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable),
                                                             ascending ? "OrderBy" : "OrderByDescending",
                                                             new[] { type, property.PropertyType }, source.Expression,
                                                             Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<TEntity> WhereContains<TEntity>(this IQueryable<TEntity> query, string field, string value) where TEntity : class
        {
            return WhereContains(query, field, value, false, false);
        }
        public static IQueryable<TEntity> WhereContains<TEntity>(this IQueryable<TEntity> query, string field, string value, bool throwExceptionIfNoProperty) where TEntity : class
        {
            return WhereContains(query, field, value, throwExceptionIfNoProperty, false);
        }
        public static IQueryable<TEntity> WhereContains<TEntity>(this IQueryable<TEntity> query, string field, string value, bool throwExceptionIfNoProperty, bool throwExceptionIfNoType) where TEntity : class
        {
            PropertyInfo propertyInfo = typeof(TEntity).GetProperty(field);
            if (propertyInfo != null)
            {
                var typeCode = Type.GetTypeCode(propertyInfo.PropertyType);
                switch (typeCode)
                {
                    case TypeCode.String:
                        return query.Where(string.Format("{0}.Contains(@0)", field), value);
                    case TypeCode.Boolean:
                        var boolValue = (value != null
                            && (value == "1" || value.ToLowerInvariant() == "true"))
                            ? true
                            : false;
                        return query.Where(string.Format("{0} == @0", field), boolValue);
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        return query.Where(string.Format("{0}.ToString().Contains(@0)", field), value);

                    // todo: DateTime, float, double, decimals, and other types.

                    default:
                        if (throwExceptionIfNoType)
                            throw new NotSupportedException(string.Format("Type '{0}' not supported.", typeCode));
                        break;
                }
            }
            else
            {
                if (throwExceptionIfNoProperty)
                    throw new NotSupportedException(string.Format("Property '{0}' not found.", field));
            }
            return query;
        }
        public static IQueryable<TEntity> WhereContains<TEntity>(this IQueryable<TEntity> query, IEnumerable<PropertyInfo> fields, string value) where TEntity : class
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            foreach (PropertyInfo field in fields)
            {
                _ = queryStringBuilder.Append(string.Format("{0}.Contains(@0)", field.Name) + " ||");
            }
            
            string queryString = queryStringBuilder.ToString();
            queryString = queryString.Substring(0, queryString.Length - 2);

            return query.Where(queryString, value);
        }
    }
}