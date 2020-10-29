using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    /// <summary>
    /// Defines properties and methods that should define the process and execution of a workflow process
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TState"></typeparam>
    public interface IWorkFlowManager<T, TState>
    {
        /// <summary>
        /// The property name that holds the state of the workflow process
        /// </summary>
        Func<T, string> StatePropertyName { get; set; }
        /// <summary>
        /// The stateable record of the process
        /// </summary>
        T Record { get; set; }
        /// <summary>
        /// The workflow processes
        /// </summary>
        IEnumerable<IWorkFlowStateProcess<T, TState>> WorkFlowStateProcesses { get; set; }
        /// <summary>
        /// The method that runs the workflow processes
        /// </summary>
        /// <returns></returns>
        ValueTask<IApiResponse<T>> RunWorkFlowProcessAsync();
    }
}
