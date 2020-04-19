using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using Z.EntityFramework.Plus;

namespace OneLine.Extensions
{
    public static class QueryableExtensions
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

        public static bool IsNullOrEmpty<T>(this IQueryable<T> source)
        {
            if (source.IsNull())
            {
                return true;
            }
            return !source.Any();
        }

        public static bool IsEmpty<T>(this IQueryable<T> source)
        {
            return !source.Any();
        }

        public static bool IsNotEmpty<T>(this IQueryable<T> source)
        {
            return source.Any();
        }

        public static IQueryable<T> Paged<T>(this IQueryable<T> source, int? Page, int? PageSize, out int Count)
        {
            if (source == null)
                throw new ArgumentNullException("source is null");
            if (source.GetType().IsAnonymousType())
                throw new InvalidOperationException("source can't be an anonymous type");

            Count = source.DeferredCount().FutureValue().Value;

            Page = (Page.HasValue && Page.Value > 0) ? Page.Value : 1;
            PageSize = (PageSize.HasValue && PageSize.Value > 0) ? PageSize.Value : Count;

            return source.Skip((Page.Value - 1) * PageSize.Value).Take(PageSize.Value).Future().AsQueryable();
        }

        public static IActionResult ToJson<T>(this IQueryable<T> source)
        {
            return new ContentResult().OutputJson(source.Future().ToList());
        }

        public static IActionResult ToJsonPaged<T>(this IQueryable<T> source, int? Page, int? PageSize, out int Count)
        {
            source = source.Paged(Page, PageSize, out Count);
            return new ContentResult()
                .OutputJson
                (
                    new Paged<IEnumerable<T>>(Page.Value, PageSize.Value, Count, (Count > 0 ? source.ToList() : Enumerable.Empty<T>()))
                );
        }

        public static IActionResult ToJsonApiResponse<T>(this IQueryable<T> source, string message = null, ApiResponseStatus apiResponseStatus = ApiResponseStatus.Succeeded)
        {
            return new ContentResult()
                .OutputJson
                (
                    new ApiResponse<IList<T>>()
                    {
                        Data = source.Future().ToList(),
                        Message = message,
                        Status = apiResponseStatus
                    }
                );
        }

        public static IActionResult ToJsonPagedApiResponse<T>(this IQueryable<T> source, int? Page, int? PageSize, out int Count, string message = null, ApiResponseStatus apiResponseStatus = ApiResponseStatus.Succeeded)
        {
            source = source.Paged(Page, PageSize, out Count);
            return new ContentResult()
                .OutputJson
                (
                    new ApiResponse<IPaged<IEnumerable<T>>>()
                    {
                        Data = new Paged<IEnumerable<T>>(Page.Value, PageSize.Value, Count, (Count > 0 ? source.ToList() : Enumerable.Empty<T>())),
                        Message = message,
                        Status = apiResponseStatus
                    }
                );
        }

        public static IApiResponse<IPaged<IEnumerable<T>>> ToApiResponsePaged<T>(this IQueryable<T> source, int? Page, int? PageSize, out int Count, string message = null, ApiResponseStatus apiResponseStatus = ApiResponseStatus.Succeeded)
        {
            source = source.Paged(Page, PageSize, out Count);
            return new ApiResponse<IPaged<IEnumerable<T>>>()
            {
                Data = new Paged<IEnumerable<T>>(Page.Value, PageSize.Value, Count, (Count > 0 ? source.ToList() : Enumerable.Empty<T>())),
                Message = message,
                Status = apiResponseStatus
            };
        }

        public static ApiResponse<Paged<IEnumerable<T>>> ToApiResponsePaged<T>(this IQueryable<T> source,int? Page, int? PageSize, out int Count, IList<string> decryptFieldsOnRead, string encryptionKey, string message = null, ApiResponseStatus apiResponseStatus = ApiResponseStatus.Succeeded)
        {
            source = source.Paged(Page, PageSize, out Count);
            var data = Count > 0 ? source.ToList() : new List<T>();
            if (decryptFieldsOnRead.Any())
            {
                foreach (var record in data)
                {
                    foreach (var prop in decryptFieldsOnRead)
                    {
                        var value = record.GetType().GetProperty(prop).GetValue(record).ToString();
                        record.GetType().GetProperty(prop).SetValue(record, value.DecryptData(encryptionKey));
                    }
                }
            }

            return new ApiResponse<Paged<IEnumerable<T>>>()
            {
                Data = new Paged<IEnumerable<T>>(Page.Value, PageSize.Value, Count, data),
                Message = message,
                Status = apiResponseStatus
            };
        }

        public static IQueryable FlattenMapping<T>(this IQueryable<T> source, bool RemoveAuditableFields, params Expression<Func<T, object>>[] PropertiesToFlatten)
        {
            string DynamicSelect = "new (";
            foreach (Expression<Func<T, object>> Property in PropertiesToFlatten)
            {
                Type PropertyType = Property.Body.Type;
                MemberExpression memberExpression = Property.Body as MemberExpression;
                string[] NavigationProperties = GetNavigationsProperties<object>(Activator.CreateInstance(PropertyType));
                foreach (PropertyInfo prop in PropertyType.GetProperties())
                {
                    //remove navigations properties that exists in the current object for the flatten mapping
                    if (!NavigationProperties.Contains(prop.Name))
                    {
                        if (!RemoveAuditableFields)
                        {
                            DynamicSelect += memberExpression.Member.Name + "." + prop.Name + " AS " + PropertyType.Name + prop.Name + ",";
                        }
                        else if (IsAuditableField(prop.Name))
                        {
                            DynamicSelect += memberExpression.Member.Name + "." + prop.Name + " AS " + PropertyType.Name + prop.Name + ",";
                        }
                    }
                }
            }
            DynamicSelect = DynamicSelect.Remove(DynamicSelect.Length - 1);
            DynamicSelect += ")";
            return source.Select(DynamicSelect);
        }

        private static string[] GetNavigationsProperties<T>(object entityType)
        {
            return entityType.GetType()
                                .GetProperties()
                                .Where(p => (typeof(IEnumerable<T>).IsAssignableFrom(p.PropertyType) && p.PropertyType != typeof(string)) ||
                                        (typeof(ICollection<T>).IsAssignableFrom(p.PropertyType) && p.PropertyType != typeof(string)) ||
                                        p.PropertyType.Namespace == entityType.GetType().Namespace)
                                .Select(p => p.Name)
                                .ToArray();
        }

        private static bool IsAuditableField(string PropertyName)
        {
            return PropertyName != "CreatedOn" &&
                    PropertyName != "CreatedBy" &&
                    PropertyName != "ModifiedOn" &&
                    PropertyName != "ModifiedBy";
        }

        public static IQueryable<T> IncludeNavigationProperties<T>(this IQueryable<T> source, params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException("queryable");
            }
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                source = source.Include(includeProperty);
            }
            return source;
        }

        public static IQueryable<T> IncludeNavigationProperties<T>(this IQueryable<T> source, params string[] includeProperties) where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException("queryable");
            }
            foreach (string includeProperty in includeProperties)
            {
                source = source.Include(includeProperty);
            }
            return source;
        }

        public static IQueryable<T> WhereIsDeleted<T>(this IQueryable<T> source) where T : ISoftDeletable
        {
            return source.Where(e => e.IsDeleted);
        }

        public static IQueryable<T> WhereNotDeleted<T>(this IQueryable<T> source) where T : ISoftDeletable
        {
            return source.Where(e => !e.IsDeleted);
        }

        public static IQueryable Where<TSource, TFilter>(this IQueryable<TSource> source, TFilter? filter, Expression<Func<TSource, bool>> predicate) where TFilter : struct
        {
            if (filter.HasValue)
            {
                source = source.Where(predicate);
            }
            return source;
        }

        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, string filter, Expression<Func<TSource, bool>> predicate)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                source = source.Where(predicate);
            }
            return source;
        }

        public static IQueryable Where<TSource, TFilter>(this IQueryable<TSource> source, IEnumerable<TFilter> filter, Expression<Func<TSource, bool>> predicate)
        {
            if (filter != null && filter.Any())
            {
                source = source.Where(predicate);
            }
            return source;
        }

        public static bool PropertyExists<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) != null;
        }

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
            MethodInfo genericMethod =
              OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }

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
            MethodInfo genericMethod =
              OrderByDescendingMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }
    }
}

