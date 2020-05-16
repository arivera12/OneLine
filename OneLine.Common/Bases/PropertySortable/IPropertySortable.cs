using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IPropertySortable
    {
        Task SortBy(string sortBy);
        Task SortBy(string sortBy, bool descending);
        Task SortByAscending(string sortBy);
        Task SortByDescending(string sortBy);
    }
}
