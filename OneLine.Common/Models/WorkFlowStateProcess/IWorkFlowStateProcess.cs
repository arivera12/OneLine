using System.Threading.Tasks;

namespace OneLine.Models
{
    /// <summary>
    /// Defines a work flow state process
    /// </summary>
    /// <typeparam name="T">The type of the record</typeparam>
    /// <typeparam name="TState">The state of the record</typeparam>
    public interface IWorkFlowStateProcess<T, TState>
    {
        /// <summary>
        /// The record
        /// </summary>
        T Record { get; set; }
        /// <summary>
        /// The current state of the process
        /// </summary>
        TState CurrentState { get; set; }
        /// <summary>
        /// The next state of the process
        /// </summary>
        TState NextState { get; set; }
        /// <summary>
        /// The method which tells when to proceed to the next process and state
        /// </summary>
        /// <returns></returns>
        Task<bool> ProceedNextStateProcessWhen();
        /// <summary>
        /// The process to do to move to the next state process
        /// </summary>
        /// <returns></returns>
        Task<IApiResponse<T>> ProceedNextStateProcess();
    }
}
