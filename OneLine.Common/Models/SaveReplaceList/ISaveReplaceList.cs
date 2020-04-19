namespace OneLine.Models
{
    public interface ISaveReplaceList<T>
    {
        /// <summary>
        /// The id to be used to save the record on the query filter
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// The data to be stored
        /// </summary>
        T Data { get; set; }
    }
}
