namespace OneLine.Models
{
    /// <summary>
    /// Defines a generic model to be used as an identifier
    /// </summary>
    /// <typeparam name="T">The type of the model identifier</typeparam>
    public interface IIdentifier<T>
    {
        /// <summary>
        /// The model of typeof <typeparamref name="T"/>
        /// </summary>
        T Model { get; set; }
    }
}
