using OneLine.Contracts;
using OneLine.Models;

namespace OneLine.Bases
{
    /// <summary>
    /// Implements a work flow state process
    /// </summary>
    /// <typeparam name="T">The type of the record</typeparam>
    /// <typeparam name="TState">The state of the record</typeparam>
    public class WorkFlowStateProcess<T, TState> : IWorkFlowStateProcess<T, TState>
    {
        public WorkFlowStateProcess()
        {
        }
        /// <inheritdoc/>
        public T Record { get; set; }
        /// <inheritdoc/>
        public TState CurrentState { get; set; }
        /// <inheritdoc/>
        public TState NextState { get; set; }
        /// <inheritdoc/>
        public virtual Task<bool> ProceedNextStateProcessWhen()
        {
            return default;
        }
        /// <inheritdoc/>
        public virtual Task<IApiResponse<T>> ProceedNextStateProcess()
        {
            return default;
        }
    }
}
