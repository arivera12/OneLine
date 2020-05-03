using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IPageableNavigable
    {
        Task GoPreviousPage();
        Task GoPreviousPage(int pageSize);
        Task GoPreviousPage(string sortBy);
        Task GoPreviousPage(int pageSize, string sortBy);
        Task GoNextPage();
        Task GoNextPage(int pageSize);
        Task GoNextPage(string sortBy);
        Task GoNextPage(int pageSize, string sortBy);
        Task GoToPage(int pageIndex, int pageSize);
        Task GoToPage(int pageIndex, string sortBy);
        Task GoToPage(int pageIndex, int pageSize, string sortBy);
    }
}
