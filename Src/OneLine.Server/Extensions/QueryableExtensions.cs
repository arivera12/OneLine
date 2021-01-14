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
<<<<<<< HEAD:OneLine/Extensions/QueryableExtensions.cs
        /// <summary>
        /// Checks if <see cref="ISoftDeletable.IsDeleted"/> is <see cref="true"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
=======

        public static IApiResponse<IPaged<IEnumerable<T>>> ToPagedApiResponse<T>(this IQueryable<T> source, int? Page, int? PageSize, out int Count, IEnumerable<string> decryptFieldsOnRead, string encryptionKey, string message = null, ApiResponseStatus apiResponseStatus = ApiResponseStatus.Succeeded)
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

>>>>>>> 98bc3b600e36b06a1909b67d7dfdaaf659019e16:Src/OneLine.Server/Extensions/QueryableExtensions.cs
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

