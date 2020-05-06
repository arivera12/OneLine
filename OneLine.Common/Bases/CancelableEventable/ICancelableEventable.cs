using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ICancelableEventable
    {
        Task Cancel();
        Action<Action> OnBeforeCancel { get; set; }
        Action OnAfterCancel { get; set; }
    }
}
