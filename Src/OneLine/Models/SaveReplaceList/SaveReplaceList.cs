namespace OneLine.Models
{
    public class SaveReplaceList<TId, T> : ISaveReplaceList<TId, T>
    {
        /// <summary>
        /// The id to be used to save the record on the query filter
        /// </summary>
        public TId Id { get; set; }
        /// <summary>
        /// The data to be stored
        /// </summary>
        public T Data { get; set; }
        public SaveReplaceList()
        {
        }
        public SaveReplaceList(TId id, T data)
        {
            Id = id;
            Data = data;
        }
    }
}
