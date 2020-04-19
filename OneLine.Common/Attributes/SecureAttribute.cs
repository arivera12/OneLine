using System;

namespace OneLine.Attributes
{
    /// <summary>
    /// This attributes marks a field as he needs to be secured by using a encription algorithm
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class SecureAttribute : Attribute
    {
        public SecureAttribute()
        {
        }
    }
}
