using OneLine.Attributes;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace OneLine.Extensions
{
    public static class BasicExtensions
    {
        public static ApiResponse<T> ToApiResponse<T>(this T objType) where T : class
        {
            return new ApiResponse<T> { Status = ApiResponseStatus.Succeeded, Data = objType };
        }

        public static ApiResponse<T> ToApiResponseFailed<T>(this T objType) where T : class
        {
            return new ApiResponse<T> { Status = ApiResponseStatus.Failed, Data = objType };
        }

        public static ApiResponse<T> ToApiResponse<T>(this T objType, ApiResponseStatus status) where T : class
        {
            return new ApiResponse<T> { Status = status, Data = objType };
        }

        public static ApiResponse<T> ToApiResponse<T>(this T objType, string message) where T : class
        {
            return new ApiResponse<T> { Status = ApiResponseStatus.Succeeded, Data = objType, Message = message };
        }

        public static ApiResponse<T> ToApiResponseFailed<T>(this T objType, string message) where T : class
        {
            return new ApiResponse<T> { Status = ApiResponseStatus.Failed, Data = objType, Message = message };
        }

        public static ApiResponse<T> ToApiResponse<T>(this T objType, ApiResponseStatus status, string message) where T : class
        {
            return new ApiResponse<T> { Status = status, Data = objType, Message = message };
        }

        public static ApiResponse<T> ToApiResponse<T>(this T objType, string message, IEnumerable<string> errorMessages) where T : class
        {
            return new ApiResponse<T> { Status = ApiResponseStatus.Failed, Data = objType, Message = message, ErrorMessages = errorMessages };
        }

        public static ApiResponse<T> ToApiResponse<T>(this T objType, IList<string> decryptFieldsOnRead, string encryptionKey) where T : class
        {
            if (decryptFieldsOnRead.Any() && string.IsNullOrWhiteSpace(encryptionKey))
            {
                throw new ArgumentNullException("The EncryptionKey is null, empty or whitespace.");
            }
            foreach (var prop in decryptFieldsOnRead)
            {
                var value = objType.GetType().GetProperty(prop).GetValue(objType).ToString();
                objType.GetType().GetProperty(prop).SetValue(objType, value.DecryptData(encryptionKey));
            }
            return new ApiResponse<T> { Status = ApiResponseStatus.Succeeded, Data = objType };
        }

        public static ApiResponse<T> ToApiResponse<T>(this T objType, IList<string> decryptFieldsOnRead, string encryptionKey, string message) where T : class
        {
            if (decryptFieldsOnRead.Any() && string.IsNullOrWhiteSpace(encryptionKey))
            {
                throw new ArgumentNullException("The EncryptionKey is null, empty or whitespace.");
            }
            foreach (var prop in decryptFieldsOnRead)
            {
                var value = objType.GetType().GetProperty(prop).GetValue(objType).ToString();
                objType.GetType().GetProperty(prop).SetValue(objType, value.DecryptData(encryptionKey));
            }
            return new ApiResponse<T> { Status = ApiResponseStatus.Succeeded, Data = objType, Message = message };
        }

        public static IEnumerable<ToT> AutoMap<FromT, ToT>(this IEnumerable<FromT> collection, bool trimStrings = true, bool autoCapitalizeStrings = false)
            where FromT : class
            where ToT : class
        {
            IList<ToT> toTModels = Activator.CreateInstance<List<ToT>>();
            foreach (var item in collection)
            {
                ToT toTModel = Activator.CreateInstance<ToT>();
                toTModels.Add(toTModel.AutoMap(item, trimStrings, autoCapitalizeStrings));
            }
            return toTModels.AsEnumerable();
        }

        public static T AutoMap<T>(this T Destination, object Source, bool trimStrings = true, bool autoCapitalizeStrings = false)
            where T : class
        {
            foreach (PropertyInfo SourceProp in Source.GetType().GetProperties())
            {
                var DestinationProp = Destination.GetType().GetProperty(SourceProp.Name);
                if (DestinationProp != null &&
                    DestinationProp.SetMethod.IsPublic &&
                    DestinationProp.CanWrite &&
                    SourceProp.GetMethod.IsPublic &&
                    SourceProp.CanRead &&
                    DestinationProp.GetType() == SourceProp.GetType() &&
                    DestinationProp.GetType().IsAssignableFrom(SourceProp.GetType())
                    )
                {
                    var value = SourceProp.GetValue(Source);
                    if (value != null && SourceProp.PropertyType == typeof(string))
                    {
                        if (trimStrings)
                            value = value.ToString().Trim();
                        if (autoCapitalizeStrings)
                            value = value.ToString().ToUpper();
                    }
                    DestinationProp.SetValue(Destination, value);
                }
            }
            return Destination;
        }

        /// <summary>
        /// This method cast an object properties into a new object casting enums with their Enum Field Nae Attribue if exists and to string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objType"></param>
        /// <returns></returns>
        public static object CastObjectEnumsAndFieldsToString<T>(this T objType) where T : class
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            foreach (PropertyInfo prop in objType.GetType().GetProperties())
            {
                string key = prop.Name;
                object value = null;
                if (prop.PropertyType.IsEnum)
                {
                    var enumFieldNameAttributes = prop.PropertyType.GetCustomAttributes(false).ToList();
                    var propValue = prop.GetValue(objType);
                    if (propValue != null)
                    {
                        if (enumFieldNameAttributes.Count > 0)
                        {
                            foreach (var item in enumFieldNameAttributes)
                            {
                                if (item.GetType() == typeof(EnumFieldNameAttribute))
                                {
                                    value = propValue.ToString();
                                    break;
                                }
                                else
                                {
                                    value = (int)propValue;
                                }
                            }
                        }
                        else
                        {
                            value = (int)propValue;
                        }
                    }
                }
                else
                {
                    value = prop.GetValue(objType)?.ToString();
                }
                keyValuePairs.Add(key, value);
            }
            return keyValuePairs;
        }

        /// <summary>
        /// This method encrypt an object fields with the secure attribute using the provided encryption key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objType"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public static T Seal<T>(this T objType, string encryptionKey) where T : class
        {
            if (string.IsNullOrWhiteSpace(encryptionKey))
            {
                throw new ArgumentNullException("The encryptionKey is null, empty or whitespace.");
            }
            foreach (PropertyInfo prop in objType.GetType().GetProperties())
            {
                var enumFieldNameAttributes = prop.PropertyType.GetCustomAttributes(false).ToList();
                var propValue = prop.GetValue(objType);
                if (propValue != null)
                {
                    if (enumFieldNameAttributes.Count > 0)
                    {
                        foreach (var item in enumFieldNameAttributes)
                        {
                            if (item.GetType() == typeof(SecureAttribute))
                            {
                                prop.SetValue(objType, propValue.ToString().EncryptData(encryptionKey));
                            }
                        }
                    }
                }
            }
            return objType;
        }

        /// <summary>
        /// This method decrypts an object fields with the secure attribute using the provided encryption key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objType"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public static T UnSeal<T>(this T objType, string encryptionKey) where T : class
        {
            if (string.IsNullOrWhiteSpace(encryptionKey))
            {
                throw new ArgumentNullException("The encryptionKey is null, empty or whitespace.");
            }
            foreach (PropertyInfo prop in objType.GetType().GetProperties())
            {
                var enumFieldNameAttributes = prop.PropertyType.GetCustomAttributes(false).ToList();
                var propValue = prop.GetValue(objType);
                if (propValue != null)
                {
                    if (enumFieldNameAttributes.Count > 0)
                    {
                        foreach (var item in enumFieldNameAttributes)
                        {
                            if (item.GetType() == typeof(SecureAttribute))
                            {
                                prop.SetValue(objType, propValue.ToString().DecryptData(encryptionKey));
                            }
                        }
                    }
                }
            }
            return objType;
        }

        /// <summary>
        /// This method encrypt an enumerable of objects fields with the secure attribute using the provided encryption key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objType"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public static IEnumerable<T> Seal<T>(this IEnumerable<T> objTypes, string encryptionKey) where T : class
        {
            if (string.IsNullOrWhiteSpace(encryptionKey))
            {
                throw new ArgumentNullException("The encryptionKey is null, empty or whitespace.");
            }
            foreach (var objType in objTypes)
            {
                foreach (PropertyInfo prop in objType.GetType().GetProperties())
                {
                    var enumFieldNameAttributes = prop.PropertyType.GetCustomAttributes(false).ToList();
                    var propValue = prop.GetValue(objType);
                    if (propValue != null)
                    {
                        if (enumFieldNameAttributes.Count > 0)
                        {
                            foreach (var item in enumFieldNameAttributes)
                            {
                                if (item.GetType() == typeof(SecureAttribute))
                                {
                                    prop.SetValue(objType, propValue.ToString().EncryptData(encryptionKey));
                                }
                            }
                        }
                    }
                }
            }
            return objTypes;
        }
        /// <summary>
        /// This method decrypt an enumerable object fields with the secure attribute using the provided encryption key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objType"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public static IEnumerable<T> UnSeal<T>(this IEnumerable<T> objTypes, string encryptionKey) where T : class
        {
            if (string.IsNullOrWhiteSpace(encryptionKey))
            {
                throw new ArgumentNullException("The encryptionKey is null, empty or whitespace.");
            }
            foreach (var objType in objTypes)
            {
                foreach (PropertyInfo prop in objType.GetType().GetProperties())
                {
                    var enumFieldNameAttributes = prop.PropertyType.GetCustomAttributes(false).ToList();
                    var propValue = prop.GetValue(objType);
                    if (propValue != null)
                    {
                        if (enumFieldNameAttributes.Count > 0)
                        {
                            foreach (var item in enumFieldNameAttributes)
                            {
                                if (item.GetType() == typeof(SecureAttribute))
                                {
                                    prop.SetValue(objType, propValue.ToString().DecryptData(encryptionKey));
                                }
                            }
                        }
                    }
                }
            }
            return objTypes;
        }
        public static string ToUrlQueryString<T>(this T obj, string separator = ",") where T : class
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            // Get all properties on the object
            var properties = obj.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(obj, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(obj, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
        public static IDictionary<string, object> ToDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
        }
        public static T ToType<T>(this IDictionary<string, object> source) where T : class, new()
        {
            var objectType = new T();
            var someObjectType = objectType.GetType();
            foreach (var item in source)
            {
                if (item.Value != null)
                {
                    someObjectType
                     .GetProperty(item.Key)
                     .SetValue(objectType, Convert.ChangeType(item.Value, someObjectType.GetProperty(item.Key).PropertyType), null);
                }
            }
            return objectType;
        }
        public static string ToUrlQueryString<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector, string separator = ",")
        {
            if (enumerable == null)
                throw new ArgumentNullException("request");

            if (selector == null)
                throw new ArgumentNullException("selector");

            return string.Join("&", enumerable.Select(selector).Select(s => (s as object).ToUrlQueryString(separator)));
        }
        public static bool IsNotNull<T>(this T source)
        {
            return source != null;
        }
        public static bool IsNull<T>(this T source)
        {
            return source == null;
        }
        public static void ThrowIfNull<T>(this T source, string parameterName) where T : class
        {
            source.ThrowIfNull(parameterName, string.Empty);
        }
        public static void ThrowIfNull<T>(this T source, string parameterName, string message) where T : class
        {
            if (source == null)
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException(parameterName);
                }
                else
                {
                    throw new ArgumentNullException(parameterName, message);
                }
            }
        }
        public static bool ModelStateIsValid<T>(this T source, out ICollection<ValidationResult> validationResults)
        {
            if (source == null)
                throw new ArgumentNullException("The source is null");

            ValidationContext validationContext = new ValidationContext(source);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            var IsValid = Validator.TryValidateObject(source, validationContext, results, true);
            validationResults = results;
            return IsValid;
        }
        public static bool IsSimpleType(this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new Type[] {
                typeof(string),
                typeof(decimal),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
    }
}