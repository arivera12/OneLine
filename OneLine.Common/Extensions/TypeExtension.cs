using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace OneLine.Extensions
{
    public static class TypeExtension
    {
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
