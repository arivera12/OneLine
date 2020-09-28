using FluentValidation;
using Newtonsoft.Json;
using OneLine.Attributes;
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
        public static string ToUrlQueryString<T>(this T obj) where T : class
        {
            if (obj is IEnumerable || obj.GetType().IsAssignableFrom(typeof(IEnumerable)))
            {
                return string.Join("&", (obj as IEnumerable<object>).Select(s => s == null ? "" : s.ToUrlQueryString()));
            }
            else
            {
                var serialize = JsonConvert.SerializeObject(obj);
                var deserialize = JsonConvert.DeserializeObject<IDictionary<string, object>>(serialize);
                var queryString = deserialize.Select(x => Uri.EscapeDataString(x.Key ?? "") + "=" + Uri.EscapeDataString(x.Value?.ToString() ?? "") ?? "");
                return string.Join("&", queryString);
            }
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
        public static IEnumerable<T> ToEnumerable<T>(this T input)
        {
            yield return input;
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
        public static bool IsComplextType(this Type type)
        {
            return !type.IsSimpleType();
        }
        public static bool PropertyExists<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) != null;
        }
        public static async Task<IApiResponse<T>> ValidateAsync<T>(this T record, IValidator validator)
            where T : class
        {
            if (record == null)
            {
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, "RecordIsNull");
            }
            if (validator == null)
            {
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, "ValidatorIsNull");
            }
            var validationResult = await validator.ValidateAsync(record);
            if (!validationResult.IsValid)
            {
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, validationResult.Errors.Select(x => x.ErrorMessage));
            }
            return new ApiResponse<T>(ApiResponseStatus.Succeeded, record);
        }
        public static async Task<IApiResponse<T>> ValidateAsync<T>(this T record, IValidator validator, string userId)
            where T : class
        {
            if (record == null)
            {
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, "RecordIsNull");
            }
            if (validator == null)
            {
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, "ValidatorIsNull");
            }
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, "UserIdIsNullOrEmpty");
            }
            var validationResult = await validator.ValidateAsync(record);
            if (!validationResult.IsValid)
            {
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, validationResult.Errors.Select(x => x.ErrorMessage));
            }
            return new ApiResponse<T>(ApiResponseStatus.Succeeded, record);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> ValidateRangeAsync<T>(this IEnumerable<T> records, IValidator validator)
            where T : class
        {
            if (records == null || !records.Any())
            {
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, "RecordIsNull");
            }
            if (validator == null)
            {
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, "ValidatorIsNull");
            }
            var validationResult = await validator.ValidateAsync(records);
            if (!validationResult.IsValid)
            {
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, validationResult.Errors.Select(x => x.ErrorMessage));
            }
            return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Succeeded, records);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> ValidateRangeAsync<T>(this IEnumerable<T> records, IValidator validator, string userId)
            where T : class
        {
            if (records == null || !records.Any())
            {
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, "RecordIsNull");
            }
            if (validator == null)
            {
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, "ValidatorIsNull");
            }
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, "UserIdIsNullOrEmpty");
            }
            var validationResult = await validator.ValidateAsync(records);
            if (!validationResult.IsValid)
            {
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, validationResult.Errors.Select(x => x.ErrorMessage));
            }
            return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Succeeded, records);
        }
    }
}