using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IResettableEventable
    {
        Task Reset();
        Action<Action> OnBeforeReset { get; set; }
        Action OnAfterReset { get; set; }
    }
}
