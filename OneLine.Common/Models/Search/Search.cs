namespace OneLine.Models
{
    /// <summary>
    /// Implements a structure with a search term
    /// </summary>
    public class Search : ISearch
    {
        /// <inheritdoc/>
        public virtual string SearchTerm { get; set; }
    }
}
