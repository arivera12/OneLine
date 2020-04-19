

namespace OneLine.Models
{
    public class SearchPaging : ISearchPaging
    {
        /// <summary>
        /// Sets a search term to be used in the query
        /// </summary>
        public virtual string SearchTerm { get; set; }
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
    }
}
