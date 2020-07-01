using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ISearchableEventable
    {
        Action OnBeforeSearch { get; set; }
        Task Search();
        Action OnAfterSearch { get; set; }
        bool InitialAutoSearch { get; set; }
    }
}
