using FluentValidation;
using Newtonsoft.Json;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static class BasicExtensions
    {
        /// <summary>
        /// Converts <typeparamref name="T"/> to a <see cref="ApiResponse{T}" /> with succeeded status 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="objType">The object type</param>
        /// <returns></returns>
        public static ApiResponse<T> ToApiResponse<T>(this T objType) where T : class
        {
            return new ApiResponse<T> { Status = ApiResponseStatus.Succeeded, Data = objType };
        }
        /// <summary>
        /// Converts <typeparamref name="T"/> to a <see cref="ApiResponse{T}" /> with failed status 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="objType">The object type</param>
        /// <returns></returns>
        public static ApiResponse<T> ToApiResponseFailed<T>(this T objType) where T : class
        {
            return new ApiResponse<T> { Status = ApiResponseStatus.Failed, Data = objType };
        }
        /// <summary>
        /// Converts <typeparamref name="T"/> to a <see cref="ApiResponse{T}" />
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="status">The api response status</param>
        /// <returns></returns>
        public static ApiResponse<T> ToApiResponse<T>(this T objType, ApiResponseStatus status) where T : class
        {
            return new ApiResponse<T> { Status = status, Data = objType };
        }
        /// <summary>
        /// Converts <typeparamref name="T"/> to a <see cref="ApiResponse{T}" /> with succeeded status 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="message">The message</param>
        /// <returns></returns>
        public static ApiResponse<T> ToApiResponse<T>(this T objType, string message) where T : class
        {
            return new ApiResponse<T> { Status = ApiResponseStatus.Succeeded, Data = objType, Message = message };
        }
        /// <summary>
        /// Converts <typeparamref name="T"/> to a <see cref="ApiResponse{T}" /> with failed status 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="message">The message</param>
        /// <returns></returns>
        public static ApiResponse<T> ToApiResponseFailed<T>(this T objType, string message) where T : class
        {
            return new ApiResponse<T> { Status = ApiResponseStatus.Failed, Data = objType, Message = message };
        }
        /// <summary>
        /// Converts <typeparamref name="T"/> to a <see cref="ApiResponse{T}" />
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="status">The status</param>
        /// <param name="message">The message</param>
        /// <returns></returns>
        public static ApiResponse<T> ToApiResponse<T>(this T objType, ApiResponseStatus status, string message) where T : class
        {
            return new ApiResponse<T> { Status = status, Data = objType, Message = message };
        }
        /// <summary>
        /// Converts <typeparamref name="T"/> to a <see cref="ApiResponse{T}" /> with failed status 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="message">The message</param>
        /// <param name="errorMessages">The error messags</param>
        /// <returns></returns>
        public static ApiResponse<T> ToApiResponse<T>(this T objType, string message, IEnumerable<string> errorMessages) where T : class
        {
            return new ApiResponse<T> { Status = ApiResponseStatus.Failed, Data = objType, Message = message, ErrorMessages = errorMessages };
        }
        /// <summary>
        /// Maps to <typeparamref name="T"/>. Take note that field names, types and accessfiers must be equal.
        /// </summary>
        /// <typeparam name="T">The to type that will be converted</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="trimStrings">whether to perform trim operation on <see cref="string"/></param>
        /// <param name="autoUpperCaseStrings">whether to uppercase the <see cref="string"/> values</param>
        /// <returns></returns>
        public static IEnumerable<T> AutoMap<T>(this IEnumerable<object> collection, bool trimStrings = true, bool autoUpperCaseStrings = false)
        {
            IList<T> toTModels = Activator.CreateInstance<List<T>>();
            foreach (var item in collection)
            {
                T toTModel = Activator.CreateInstance<T>();
                toTModels.Add(toTModel.AutoMap(item, trimStrings, autoUpperCaseStrings));
            }
            return toTModels.AsEnumerable();
        }
        /// <summary>
        /// Maps the <paramref name="source"/> object to <typeparamref name="T"/>. Take note that field names, types and accessfiers must be equal.
        /// </summary>
        /// <typeparam name="T">The destination type that will be converted</typeparam>
        /// <param name="source">The source type that will be converted to <typeparamref name="T"/></param>
        /// <param name="destination">The destination type that will be converted</param>
        /// <param name="trimStrings">whether to perform trim operation on <see cref="string"/></param>
        /// <param name="autoUpperCaseStrings">whether to uppercase the <see cref="string"/> values</param>
        /// <returns></returns>
        public static T AutoMap<T>(this T destination, object source, bool trimStrings = true, bool autoUpperCaseStrings = false)
        {
            foreach (PropertyInfo SourceProp in source.GetType().GetProperties())
            {
                var DestinationProp = destination.GetType().GetProperty(SourceProp.Name);
                if (DestinationProp != null &&
                    DestinationProp.SetMethod.IsPublic &&
                    DestinationProp.CanWrite &&
                    SourceProp.GetMethod.IsPublic &&
                    SourceProp.CanRead &&
                    DestinationProp.GetType() == SourceProp.GetType() &&
                    DestinationProp.GetType().IsAssignableFrom(SourceProp.GetType())
                    )
                {
                    var value = SourceProp.GetValue(source);
                    if (value != null && SourceProp.PropertyType == typeof(string))
                    {
                        if (trimStrings)
                            value = value.ToString().Trim();
                        if (autoUpperCaseStrings)
                            value = value.ToString().ToUpper();
                    }
                    DestinationProp.SetValue(destination, value);
                }
            }
            return destination;
        }
        /// <summary>
        /// Converts <typeparamref name="T"/> to a url query string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToUrlQueryString<T>(this T obj) where T : class
        {
            if (obj is IEnumerable || obj.GetType().IsAssignableFrom(typeof(IEnumerable)))
            {
                return string.Join("&", (obj as IEnumerable<object>).Select(s => s.IsNull() ? "" : s.ToUrlQueryString()));
            }
            else
            {
                var serialize = JsonConvert.SerializeObject(obj);
                var deserialize = JsonConvert.DeserializeObject<IDictionary<string, object>>(serialize);
                var queryString = deserialize.Select(x => Uri.EscapeDataString(x.Key ?? "") + "=" + Uri.EscapeDataString(x.Value?.ToString() ?? "") ?? "");
                return string.Join("&", queryString);
            }
        }
        /// <summary>
        /// Converts <typeparamref name="T"/> to a <see cref="IDictionary{string, object}"/>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary<T>(this T source, BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
            where T : class
        {
            return source.GetType().GetProperties(bindingFlags).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
        }
        /// <summary>
        /// Converts <see cref="IDictionary{string, object}"/> to <typeparamref name="T"/> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ToType<T>(this IDictionary<string, object> source) where T : class, new()
        {
            var objectType = new T();
            var someObjectType = objectType.GetType();
            foreach (var item in source)
            {
                if (item.Value.IsNotNull())
                {
                    someObjectType
                     .GetProperty(item.Key)
                     .SetValue(objectType, Convert.ChangeType(item.Value, someObjectType.GetProperty(item.Key).PropertyType), null);
                }
            }
            return objectType;
        }
        /// <summary>
        /// Converts to <typeparamref name="T"/>
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ToType<T>(this object source)
        {
            return (T)source;
        }
        /// <summary>
        /// Checks whether <typeparamref name="T"/> is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T source)
        {
            return source == null;
        }
        /// <summary>
        /// Checks whether <typeparamref name="T"/> is not null
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="source">The sources</param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this T source)
        {
            return !source.IsNull();
        }
        /// <summary>
        /// Checks whether the type is a simple type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns></returns>
        public static bool IsSimpleType(this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new Type[] {
                typeof(short),
                typeof(float),
                typeof(string),
                typeof(decimal),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
        /// <summary>
        /// Checks whether the type is a complex type 
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns></returns>
        public static bool IsComplextType(this Type type)
        {
            return !type.IsSimpleType();
        }
        /// <summary>
        /// Checks whether the <paramref name="propertyName"/> exists in <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="propertyName">The property name</param>
        /// <param name="bindingFlags">The binding flags</param>
        /// <returns></returns>
        public static bool PropertyExists<T>(string propertyName, BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
        {
            return typeof(T).GetProperty(propertyName, bindingFlags).IsNotNull();
        }
        /// <summary>
        /// Validates <typeparamref name="T"/> using the <paramref name="validator"/>
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="source">The source</param>
        /// <param name="validator">The validator</param>
        /// <returns></returns>
        public static async Task<IApiResponse<T>> ValidateAsync<T>(this T source, IValidator validator)
            where T : class
        {
            if (source.IsNull())
            {
                return new ApiResponse<T>(ApiResponseStatus.Failed, source, "RecordIsNull");
            }
            if (validator.IsNull())
            {
                return new ApiResponse<T>(ApiResponseStatus.Failed, source, "ValidatorIsNull");
            }
            var validationResult = await validator.ValidateAsync(source);
            if (!validationResult.IsValid)
            {
                return new ApiResponse<T>(ApiResponseStatus.Failed, source, validationResult.Errors.Select(x => x.ErrorMessage));
            }
            return new ApiResponse<T>(ApiResponseStatus.Succeeded, source);
        }
        /// <summary>
        /// Validates <paramref name="source"/> <typeparamref name="T"/> using the <paramref name="validator"/>
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="source">The source collection</param>
        /// <param name="validator">The validator</param>
        /// <returns></returns>
        public static async Task<IApiResponse<IEnumerable<T>>> ValidateRangeAsync<T>(this IEnumerable<T> source, IValidator validator)
            where T : class
        {
            if (source.IsNull() || !source.Any())
            {
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, source, "RecordIsNull");
            }
            if (validator.IsNull())
            {
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, source, "ValidatorIsNull");
            }
            var validationResult = await validator.ValidateAsync(source);
            if (!validationResult.IsValid)
            {
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, source, validationResult.Errors.Select(x => x.ErrorMessage));
            }
            return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Succeeded, source);
        }
        /// <summary>
        /// Creates a void context of <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="context">The context</param>
        /// <param name="action">The Action</param>
        public static void ToContextVoid<T>(this T context, Action<T> action)
        {
            action(context);
        }
        /// <summary>
        /// Creates a context of <typeparamref name="T"/> and return a <typeparamref name="TResult"/>
        /// </summary>
        /// <typeparam name="T">The context type</typeparam>
        /// <typeparam name="TResult">The returning type of the context type</typeparam>
        /// <param name="context">The context</param>
        /// <param name="func">The function with returning value</param>
        /// <returns></returns>
        public static TResult ToContext<T, TResult>(this T context, Func<T, TResult> func)
        {
            return func(context);
        }
    }
}