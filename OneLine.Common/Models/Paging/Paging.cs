namespace OneLine.Models
{
    public class Paging : IPaging
    {
        /// <summary>
        /// Determines wether data should be decending
        /// </summary>
        public virtual bool? Descending { get; set; }
        /// <summary>
        /// Sets the page index of the data
        /// </summary>
        public virtual int? PageIndex { get; set; }
        /// <summary>
        /// Sets the page size of the data
        /// </summary>
        public virtual int? PageSize { get; set; }
        /// <summary>
        /// Sets whichs property of the data model wants to be used for sorting
        /// </summary>
        public virtual string SortBy { get; set; }
        public Paging()
        {

        }
        public Paging(int pageIndex, int pageSize, bool descending, string sortBy)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Descending = descending;
            SortBy = sortBy;
        }
    }
}
