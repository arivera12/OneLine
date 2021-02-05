namespace OneLine.Models
{
    /// <summary>
    /// Implements a generic model to be used as an identifier
    /// </summary>
    /// <typeparam name="T">The type of the model identifier</typeparam>
    public class Identifier<T> : IIdentifier<T>
    {
        /// <inheritdoc/>
        public T Model { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public Identifier()
        { }
        /// <summary>
        /// Contructor with model value setup
        /// </summary>
        /// <param name="model">The value of the model</param>
        public Identifier(T model)
        {
            Model = model;
        }
    }
}
