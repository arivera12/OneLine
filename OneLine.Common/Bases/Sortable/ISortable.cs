using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ISortable
    {
        Task Sort();
        Task Sort(bool descending);
        Task SortAscending();
        Task SortDescending();
    }
}
