using System.Threading.Tasks;

namespace OneLine.Bases
{
    /// <summary>
    /// Defines methods that works with a paged data and it's navigation.
    /// </summary>
    public interface IPageableNavigable
    {
        /// <summary>
        /// Navigates the previous page
        /// </summary>
        /// <returns></returns>
        Task GoPreviousPage();
        /// <summary>
        /// Navigates previous page changing the <paramref name="pageSize"/>
        /// </summary>
        /// <param name="pageSize">The page size</param>
        /// <returns></returns>
        Task GoPreviousPage(int pageSize);
        /// <summary>
        /// Navigates previous page changing the <paramref name="sortBy"/>
        /// </summary>
        /// <param name="sortBy">The sort by property name</param>
        /// <returns></returns>
        Task GoPreviousPage(string sortBy);
        /// <summary>
        /// Navigates previous page changing the <paramref name="pageSize"/> and <paramref name="sortBy"/>
        /// </summary>
        /// <param name="pageSize">The page size</param>
        /// <param name="sortBy">The sort by property name</param>
        /// <returns></returns>
        Task GoPreviousPage(int pageSize, string sortBy);
        /// <summary>
        /// Navigates next page
        /// </summary>
        /// <returns></returns>
        Task GoNextPage();
        /// <summary>
        /// Navigates next page changing the <paramref name="pageSize"/>
        /// </summary>
        /// <param name="pageSize">The page size</param>
        /// <returns></returns>
        Task GoNextPage(int pageSize);
        /// <summary>
        /// Navigates next page changing the <paramref name="sortBy"/>
        /// </summary>
        /// <param name="sortBy">The sort by property name</param>
        /// <returns></returns>
        Task GoNextPage(string sortBy);
        /// <summary>
        /// Navigates next page changing the <paramref name="pageSize"/> and <paramref name="sortBy"/>
        /// </summary>
        /// <param name="pageSize">The page size</param>
        /// <param name="sortBy">The sort by property name</param>
        /// <returns></returns>
        Task GoNextPage(int pageSize, string sortBy);
        /// <summary>
        /// Navigates to the specified page by the <paramref name="pageIndex"/> and <paramref name="pageSize"/>
        /// </summary>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <returns></returns>
        Task GoToPage(int pageIndex, int pageSize);
        /// <summary>
        /// Navigates to the specified page by the <paramref name="pageIndex"/> and <paramref name="sortBy"/>
        /// </summary>
        /// <param name="pageIndex">The page index</param>
        /// <param name="sortBy">The sort by property name</param>
        /// <returns></returns>
        Task GoToPage(int pageIndex, string sortBy);
        /// <summary>
        /// Navigates to the specified page by the <paramref name="pageIndex"/>, <paramref name="pageSize"/> and <paramref name="sortBy"/>
        /// </summary>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="sortBy">The sort by property name</param>
        /// <returns></returns>
        Task GoToPage(int pageIndex, int pageSize, string sortBy);
    }
}
