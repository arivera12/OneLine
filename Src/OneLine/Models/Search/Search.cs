namespace OneLine.Models
{
    /// <summary>
    /// Implements a structure with a search term
    /// </summary>
    public class Search : ISearch
    {
        /// <inheritdoc/>
        public string SearchTerm { get; set; }
    }
}
