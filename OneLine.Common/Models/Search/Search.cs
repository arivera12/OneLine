

namespace OneLine.Models
{
    public class Search : ISearch
    {
        /// <summary>
        /// Sets a search term to be used in the query
        /// </summary>
        public virtual string SearchTerm { get; set; }
    }
}
