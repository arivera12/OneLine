namespace OneLine.Models
{
    /// <summary>
    /// Defines a generic data holder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataHolder<T>
    {
        /// <summary>
        /// The data
        /// </summary>
        T Data { get; set; }
    }
}
