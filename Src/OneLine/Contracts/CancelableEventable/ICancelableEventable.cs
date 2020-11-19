using System;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface ICancelableEventable
    {
        /// <summary>
        /// The cancel task
        /// </summary>
        /// <returns></returns>
        Task Cancel();
        /// <summary>
        /// The action before cancel
        /// </summary>
        Action OnBeforeCancel { get; set; }
        /// <summary>
        /// The action after cancel
        /// </summary>
        Action OnAfterCancel { get; set; }
    }
}
