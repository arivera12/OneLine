namespace OneLine.Models
{
    /// <summary>
    /// Implements a paging structure to be applied
    /// </summary>
    public class Paging : IPaging
    {
        /// <inheritdoc/>
        public virtual bool? Descending { get; set; }
        /// <inheritdoc/>
        public virtual int? PageIndex { get; set; }
        /// <inheritdoc/>
        public virtual int? PageSize { get; set; }
        /// <inheritdoc/>
        public virtual string SortBy { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public Paging()
        {

        }
        /// <summary>
        /// The paging main constructor
        /// </summary>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="descending">Descending indicator</param>
        /// <param name="sortBy">Sort by property name</param>
        public Paging(int pageIndex, int pageSize, bool descending, string sortBy)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Descending = descending;
            SortBy = sortBy;
        }
    }
}
