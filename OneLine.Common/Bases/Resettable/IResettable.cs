using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IResettable
    {
        Task Reset();
        Action<Action> OnBeforeReset { get; set; }
        Action OnAfterReset { get; set; }
    }
}
