namespace OneLine.Models
{
    public interface IPaging
    {
        /// <summary>
        /// Determines wether data should be decending
        /// </summary>
        bool? Descending { get; set; }
        /// <summary>
        /// Sets the page index of the data
        /// </summary>
        int? PageIndex { get; set; }
        /// <summary>
        /// Sets the page size of the data
        /// </summary>
        int? PageSize { get; set; }
        /// <summary>
        /// Sets whichs property of the data model wants to be used for sorting
        /// </summary>
        string SortBy { get; set; }
    }
}
