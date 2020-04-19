namespace OneLine.Models
{
    public interface ISearch
    {
        /// <summary>
        /// Sets a search term to be used in the query
        /// </summary>
        string SearchTerm { get; set; }
    }
}
