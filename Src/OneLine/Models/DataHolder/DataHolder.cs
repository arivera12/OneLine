namespace OneLine.Models
{
    /// <summary>
    /// Implements <see cref="IDataHolder{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataHolder<T> : IDataHolder<T>
    {
        /// <summary>
        /// The data
        /// </summary>
        public T Data { get; set; }
        public DataHolder()
        {
        }
        public DataHolder(T data)
        {
            Data = data;
        }
    }
}
