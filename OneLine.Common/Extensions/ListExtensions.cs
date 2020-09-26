using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace OneLine.Extensions
{
    public static class ListExtensions
    {
        public static IList<T> Clone<T>(this IList<T> source) where T : ICloneable
        {
            return source.Select(item => (T)item.Clone()).ToList();
        }

        private static readonly MethodInfo OrderByMethod =
                                typeof(Queryable)
                                    .GetMethods()
                                    .Single(method => method.Name == "OrderBy" &&
                                                        method.GetParameters().Length == 2);

        private static readonly MethodInfo OrderByDescendingMethod =
                                typeof(Queryable)
                                .GetMethods()
                                .Single(method => method.Name == "OrderByDescending" &&
                                                    method.GetParameters().Length == 2);

        public static IList<T> Paged<T>(this IList<T> source, int? Page, int? RowsPerPage)
        {
            if (source == null)
                throw new ArgumentNullException("source is null");
            if (source.GetType().IsAnonymousType())
                throw new InvalidOperationException("source can't be an anonymous type");

            return source.Skip((Page.Value - 1) * RowsPerPage.Value).Take(RowsPerPage.Value).ToList();
        }
             
        public static IList<T> WhereIsDeleted<T>(this IList<T> source) where T : ISoftDeletable
        {
            return (IList<T>)source.Where(e => e.IsDeleted);
        }

        public static IList<T> WhereNotDeleted<T>(this IList<T> source) where T : ISoftDeletable
        {
            return (IList<T>)source.Where(e => !e.IsDeleted);
        }  

        public static bool PropertyExists<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) != null;
        }

        public static IList<T> OrderByProperty<T>(this IList<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }
            ParameterExpression paramterExpression = Expression.Parameter(typeof(T));
            Expression orderByProperty = Expression.Property(paramterExpression, propertyName);
            LambdaExpression lambda = Expression.Lambda(orderByProperty, paramterExpression);
            MethodInfo genericMethod =
              OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IList<T>)ret;
        }

        public static IList<T> OrderByPropertyDescending<T>(this IList<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }
            ParameterExpression paramterExpression = Expression.Parameter(typeof(T));
            Expression orderByProperty = Expression.Property(paramterExpression, propertyName);
            LambdaExpression lambda = Expression.Lambda(orderByProperty, paramterExpression);
            MethodInfo genericMethod =
              OrderByDescendingMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IList<T>)ret;
        }
    }
}

