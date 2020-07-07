using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IWorkFlowManager<T, TState>
    {
        Func<T, string> StatePropertyName { get; set; }
        T Record { get; set; }
        IEnumerable<IWorkFlowStateProcess<T, TState>> WorkFlowStateProcesses { get; set; }
        ValueTask<IApiResponse<T>> RunWorkFlowProcessAsync();
    }
}
