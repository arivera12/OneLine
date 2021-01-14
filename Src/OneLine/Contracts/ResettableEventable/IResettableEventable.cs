using System;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    /// <summary>
    /// Define method to reset a context with before and after actions
    /// </summary>
    public interface IResettableEventable
    {
        /// <summary>
        /// The reset method. This method should be chained with <see cref="OnAfterReset"/>
        /// </summary>
        /// <returns></returns>
        Task Reset();
        /// <summary>
        /// The before reset action callback. This method should be chained with <see cref="Reset"/>
        /// </summary>
        Action OnBeforeReset { get; set; }
        /// <summary>
        /// The on after reset method. This method should be called from <see cref="Reset"/>
        /// </summary>
        Action OnAfterReset { get; set; }
    }
}
