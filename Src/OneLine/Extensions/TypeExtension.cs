using System;
using System.Collections;
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
    }
}
