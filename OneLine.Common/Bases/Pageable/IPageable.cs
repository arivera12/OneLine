using OneLine.Enums;
using OneLine.Models;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IPageable
    {
        ISearchPaging SearchPaging { get; set; }
        Task PagingFilterChange(IPaging paging);
    }
}
