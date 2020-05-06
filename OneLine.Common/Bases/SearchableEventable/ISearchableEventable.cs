using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ISearchableEventable
    {
        Action<Action> OnBeforeSearch { get; set; }
        Task Search();
        Action OnAfterSearch { get; set; }
    }
}
