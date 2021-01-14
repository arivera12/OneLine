using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OneLine.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Pageds a <see cref="IQueryable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static IQueryable<T> Paged<T>(this IQueryable<T> source, int? Page, int? PageSize, out int Count)
        {
            if (source == null)
                throw new ArgumentNullException("source is null");
            if (source.GetType().IsAnonymousType())
                throw new InvalidOperationException("source can't be an anonymous type");

            Count = source.Count();
            Page = Page.HasValue && Page.Value >= 0 ? Page.Value : 0;
            PageSize = PageSize.HasValue && PageSize.Value > 0 ? PageSize.Value : Count;

            return source.Skip(Page.Value * PageSize.Value).Take(PageSize.Value).AsQueryable();
        }
        /// <summary>
        /// Pageds a <see cref="IQueryable{T}"/> to an <see cref="IApiResponse{IPaged{IEnumerable{T}}}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="Count"></param>
        /// <param name="message"></param>
        /// <param name="apiResponseStatus"></param>
        /// <returns></returns>
        public static IApiResponse<IPaged<IEnumerable<T>>> ToPagedApiResponse<T>(this IQueryable<T> source, int? Page, int? PageSize, out int Count, string message = null, ApiResponseStatus apiResponseStatus = ApiResponseStatus.Succeeded)
        {
            source = source.Paged(Page, PageSize, out Count);
            Page = Page.HasValue && Page.Value >= 0 ? Page.Value : 0;
            PageSize = PageSize.HasValue && PageSize.Value > 0 ? PageSize.Value : Count;
            return new ApiResponse<IPaged<IEnumerable<T>>>()
            {
                Data = new Paged<IEnumerable<T>>(Page.Value, PageSize.Value, Count, Count > 0 ? source : Enumerable.Empty<T>()),
                Message = message,
                Status = apiResponseStatus
            };
        }
        /// <summary>
        /// Checks if <see cref="ISoftDeletable.IsDeleted"/> is <see cref="true"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIsDeleted<T>(this IQueryable<T> source) where T : ISoftDeletable
        {
            return source.Where(e => e.IsDeleted);
        }
        /// <summary>
        /// Checks if <see cref="ISoftDeletable.IsDeleted"/> is <see cref="false"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereNotIsDeleted<T>(this IQueryable<T> source) where T : ISoftDeletable
        {
            return source.Where(e => !e.IsDeleted);
        }
        /// <summary>
        /// Check whether a property exist in <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool PropertyExists<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) != null;
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
        /// <summary>
        /// Performs ascending sorting by a property name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }
            ParameterExpression paramterExpression = Expression.Parameter(typeof(T));
            Expression orderByProperty = Expression.Property(paramterExpression, propertyName);
            LambdaExpression lambda = Expression.Lambda(orderByProperty, paramterExpression);
            MethodInfo genericMethod = OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }
        /// <summary>
        /// Performs descending sorting by a property name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByPropertyDescending<T>(this IQueryable<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }
            ParameterExpression paramterExpression = Expression.Parameter(typeof(T));
            Expression orderByProperty = Expression.Property(paramterExpression, propertyName);
            LambdaExpression lambda = Expression.Lambda(orderByProperty, paramterExpression);
            MethodInfo genericMethod = OrderByDescendingMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }
    }
}

