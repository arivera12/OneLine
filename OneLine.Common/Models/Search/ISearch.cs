namespace OneLine.Models
{
    /// <summary>
    /// Defines a structure with a search term
    /// </summary>
    public interface ISearch
    {
        /// <summary>
        /// Sets a search term to be used in the query
        /// </summary>
        string SearchTerm { get; set; }
    }
}
