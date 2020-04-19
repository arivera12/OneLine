using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IDeletable
    {
        Task Delete();
        Action<Action> OnBeforeDelete { get; set; }
        Action OnAfterDelete { get; set; }
    }
    public interface IDeletable<T>
    {
        Task Delete();
        Action<Action> OnBeforeDelete { get; set; }
        Action<T> OnAfterDelete { get; set; }
    }
}
