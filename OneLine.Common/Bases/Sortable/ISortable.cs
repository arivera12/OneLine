using System.Threading.Tasks;

namespace OneLine.Bases
{
    /// <summary>
    /// Defines methods for sortable capabilities
    /// </summary>
    public interface ISortable
    {
        /// <summary>
        /// Toggles sorting descending or ascending
        /// </summary>
        /// <returns></returns>
        Task Sort();
        /// <summary>
        /// Sorts descending or ascending 
        /// </summary>
        /// <param name="descending"></param>
        /// <returns></returns>
        Task Sort(bool descending);
        /// <summary>
        /// Sorts ascending
        /// </summary>
        /// <returns></returns>
        Task SortAscending();
        /// <summary>
        /// Sorts descending 
        /// </summary>
        /// <returns></returns>
        Task SortDescending();
    }
}
