using System.Threading.Tasks;

namespace OneLine.Contracts
{
    /// <summary>
    /// Defines methods to sort by a property of the model context
    /// </summary>
    public interface IPropertySortable
    {
        /// <summary>
        /// Sorts the by <paramref name="sortBy"/> property name
        /// </summary>
        /// <param name="sortBy">The sort by property name</param>
        /// <returns></returns>
        Task SortBy(string sortBy);
        /// <summary>
        /// Sorts the by <paramref name="sortBy"/> property name 
        /// </summary>
        /// <param name="sortBy">The sort by property name</param>
        /// <param name="descending">Descending?</param>
        /// <returns></returns>
        Task SortBy(string sortBy, bool descending);
        /// <summary>
        /// Sorts the ascending by <paramref name="sortBy"/> property name 
        /// </summary>
        /// <param name="sortBy">The sort by property name</param>
        /// <returns></returns>
        Task SortByAscending(string sortBy);
        /// <summary>
        /// Sorts the descending by <paramref name="sortBy"/> property name 
        /// </summary>
        /// <param name="sortBy">The sort by property name</param>
        /// <returns></returns>
        Task SortByDescending(string sortBy);
    }
}
