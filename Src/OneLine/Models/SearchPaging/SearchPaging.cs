namespace OneLine.Models
{
    /// <summary>
    /// Implements a structure with search term and paging capabilities
    /// </summary>
    public class SearchPaging : ISearchPaging
    {
        /// <inheritdoc/>
        public string SearchTerm { get; set; }
        /// <inheritdoc/>
        public bool? Descending { get; set; }
        /// <inheritdoc/>
        public int? PageIndex { get; set; }
        /// <inheritdoc/>
        public int? PageSize { get; set; }
        /// <inheritdoc/>
        public string SortBy { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchPaging() { }
        /// <summary>
        /// Constructor with paging capabilities
        /// </summary>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="descending">Descending indicator</param>
        /// <param name="sortBy">Sort by property name</param>
        public SearchPaging(int? pageIndex, int? pageSize, bool? descending, string sortBy)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Descending = descending;
            SortBy = sortBy;
        }
        /// <summary>
        /// Constructor with search term and paging capabilities
        /// </summary>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="descending">Descending indicator</param>
        /// <param name="sortBy">Sort by property name</param>
        /// <param name="searchTerm">The search term</param>
        public SearchPaging(int? pageIndex, int? pageSize, bool? descending, string sortBy, string searchTerm)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Descending = descending;
            SortBy = sortBy;
            SearchTerm = searchTerm;
        }
    }
}
