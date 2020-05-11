using System;

namespace OneLine.Models
{
    public class IdentifierString : IIdentifier<string>
    {
        public virtual string Model { get; set; }
        public IdentifierString()
        { }
        public IdentifierString(string model)
        {
            Model = model;
        }
    }
}
