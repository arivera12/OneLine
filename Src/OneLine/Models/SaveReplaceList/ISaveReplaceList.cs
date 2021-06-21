namespace OneLine.Models
{
    public interface ISaveReplaceList<TId, T> : IDataHolder<T>
    {
        /// <summary>
        /// The id to be used to save the record on the query filter
        /// </summary>
        TId Id { get; set; }
    }
}
