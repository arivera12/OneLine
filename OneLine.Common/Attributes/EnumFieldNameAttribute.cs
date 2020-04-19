using System;

namespace OneLine.Attributes
{
    /// <summary>
    /// This attribute lets know to encode the enum selected value by his FieldName instead of the enum value
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    public class EnumFieldNameAttribute : Attribute
    {
        public EnumFieldNameAttribute()
        {
        }
    }
}
