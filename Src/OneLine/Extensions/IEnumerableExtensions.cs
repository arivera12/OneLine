using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OneLine.Extensions
{
    public static class IEnumerableExtensions
    {
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
        /// Sorts ascending the collection by a property name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IEnumerable<T> OrderByProperty<T>(this IEnumerable<T> source, string propertyName)
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
            return (IEnumerable<T>)ret;
        }
        /// <summary>
        /// Sorts descending the collection by a property name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IEnumerable<T> OrderByPropertyDescending<T>(this IEnumerable<T> source, string propertyName)
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
            return (IEnumerable<T>)ret;
        }
        /// <summary>
        /// Returns a <see cref="IEnumerable{T item, int index}"/> where you can loop with the item type and it's index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source) => source.Select((item, index) => (item, index));
        /// <summary>
        /// Pageds a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static IEnumerable<T> Paged<T>(this IEnumerable<T> source, int? Page, int? PageSize, out int Count)
        {
            if (source == null)
                throw new ArgumentNullException("source is null");
            if (source.GetType().IsAnonymousType())
                throw new InvalidOperationException("source can't be an anonymous type");

            Count = source.Count();
            Page = Page.HasValue && Page.Value >= 0 ? Page.Value : 0;
            PageSize = PageSize.HasValue && PageSize.Value > 0 ? PageSize.Value : Count;

            return source.Skip(Page.Value * PageSize.Value).Take(PageSize.Value);
        }
        /// <summary>
        /// Pageds a <see cref="IEnumerable{T}"/> to an <see cref="IApiResponse{IPaged{IEnumerable{T}}}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="Count"></param>
        /// <param name="message"></param>
        /// <param name="apiResponseStatus"></param>
        /// <returns></returns>
        public static IApiResponse<IPaged<IEnumerable<T>>> ToPagedApiResponse<T>(this IEnumerable<T> source, int? Page, int? PageSize, out int Count, string message = null, ApiResponseStatus apiResponseStatus = ApiResponseStatus.Succeeded)
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
        /// Pageds a <see cref="IEnumerable{T}"/> to an <see cref="IApiResponse{IPaged{IEnumerable{T}}}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="Count"></param>
        /// <param name="decryptFieldsOnRead"></param>
        /// <param name="encryptionKey"></param>
        /// <param name="message"></param>
        /// <param name="apiResponseStatus"></param>
        /// <returns></returns>
        public static IApiResponse<IPaged<IEnumerable<T>>> ToPagedApiResponse<T>(this IEnumerable<T> source, int? Page, int? PageSize, out int Count, IEnumerable<string> decryptFieldsOnRead, string encryptionKey, string message = null, ApiResponseStatus apiResponseStatus = ApiResponseStatus.Succeeded)
        {
            source = source.Paged(Page, PageSize, out Count);
            Page = Page.HasValue && Page.Value >= 0 ? Page.Value : 0;
            PageSize = PageSize.HasValue && PageSize.Value > 0 ? PageSize.Value : Count;
            var data = Count > 0 ? source : Enumerable.Empty<T>();
            if (decryptFieldsOnRead.Any())
            {
                foreach (var record in data)
                {
                    foreach (var prop in decryptFieldsOnRead)
                    {
                        var value = record.GetType().GetProperty(prop).GetValue(record).ToString();
                        record.GetType().GetProperty(prop).SetValue(record, value.Decrypt(encryptionKey));
                    }
                }
            }
            return new ApiResponse<IPaged<IEnumerable<T>>>()
            {
                Data = new Paged<IEnumerable<T>>(Page.Value, PageSize.Value, Count, data),
                Message = message,
                Status = apiResponseStatus
            };
        }
    }
}

