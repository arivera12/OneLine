using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IResettableEventable
    {
        Task Reset();
        Action OnBeforeReset { get; set; }
        Action OnAfterReset { get; set; }
    }
}
