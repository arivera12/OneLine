namespace OneLine.Models
{
    /// <summary>
    /// Implements a structure with a search term
    /// </summary>
    public class Search : ISearch
    {
        /// <inheritdoc/>
        public string SearchTerm { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public Search()
        {

        }
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="searchTerm">The search term</param>
        public Search(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}
