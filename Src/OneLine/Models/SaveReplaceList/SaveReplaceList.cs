namespace OneLine.Models
{
    public class SaveReplaceList<T> : ISaveReplaceList<T>
    {
        /// <summary>
        /// The id to be used to save the record on the query filter
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// The data to be stored
        /// </summary>
        public virtual T Data { get; set; }
    }
}
