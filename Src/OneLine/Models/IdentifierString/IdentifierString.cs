namespace OneLine.Models
{
    /// <summary>
    /// Implements a string model to be used as an identifier
    /// </summary>
    public class IdentifierString : IIdentifier<string>
    {
        /// <summary>
        /// The model is a string type
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public IdentifierString()
        { }
        /// <summary>
        /// Constructor setting the string model value
        /// </summary>
        /// <param name="model"></param>
        public IdentifierString(string model)
        {
            Model = model;
        }
    }
}
