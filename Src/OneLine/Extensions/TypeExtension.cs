using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace OneLine.Extensions
{
    public static class TypeExtension
    {
        /// <summary>
        /// Checks if the type is anonymous type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAnonymousType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (typeof(IEnumerable).IsAssignableFrom(type) || typeof(ICollection).IsAssignableFrom(type))
                type = type.GetGenericArguments()[0];
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                && type.IsGenericType && type.Name.Contains("AnonymousType")
                && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }
        /// <summary>
        ///  Determines wether the type is numeric
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNumericType(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
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
                typeof(byte),
                typeof(byte[]),
                typeof(sbyte),
                typeof(sbyte[]),
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
        public static bool PropertyExists(this Type type, string propertyName, BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
        {
            return type.GetProperty(propertyName, bindingFlags).IsNotNull();
        }
    }
}
