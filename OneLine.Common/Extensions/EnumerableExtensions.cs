using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq.Dynamic.Core;

namespace OneLine.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source.IsNull())
            {
                return true;
            }
            return !source.Any();
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        public static IEnumerable<T> Paged<T>(this IEnumerable<T> source, int? Page, int? RowsPerPage)
        {
            if (source == null)
                throw new ArgumentNullException("source is null");
            if (source.GetType().IsAnonymousType())
                throw new InvalidOperationException("source can't be an anonymous type");

            return source.Skip((Page.Value - 1) * RowsPerPage.Value).Take(RowsPerPage.Value).ToList();
        }

        public static IEnumerable<object> FlattenMapping<T>(this IEnumerable<T> source, bool RemoveAuditableFields, params Expression<Func<T, object>>[] PropertiesToFlatten)
        {
            string DynamicSelect = "new (";
            foreach (Expression<Func<T, object>> Property in PropertiesToFlatten)
            {
                Type PropertyType = Property.Body.Type;
                MemberExpression memberExpression = Property.Body as MemberExpression;
                string[] NavigationProperties = GetNavigationsPropertiess<object>(Activator.CreateInstance(PropertyType));
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
            return (IEnumerable<object>)source.AsQueryable().Select(DynamicSelect);
        }

        private static bool IsAuditableField(string PropertyName)
        {
            return PropertyName != "CreatedOn" &&
                    PropertyName != "CreatedBy" &&
                    PropertyName != "ModifiedOn" &&
                    PropertyName != "ModifiedBy";
        }

        private static string[] GetNavigationsPropertiess<T>(object entityType)
        {
            return entityType.GetType()
                                .GetProperties()
                                .Where(p => (typeof(IEnumerable<T>).IsAssignableFrom(p.PropertyType) && p.PropertyType != typeof(string)) ||
                                        (typeof(ICollection<T>).IsAssignableFrom(p.PropertyType) && p.PropertyType != typeof(string)) ||
                                        p.PropertyType.Namespace == entityType.GetType().Namespace)
                                .Select(p => p.Name)
                                .ToArray();
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

        public static bool PropertyExists<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) != null;
        }

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
    }
}

