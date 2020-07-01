using OneLine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IWorkFlowManager<T, TState>
    {
        IEnumerable<IWorkFlowStateProcess<T, TState>> WorkFlowStateProcesses { get; set; }
        ValueTask<IApiResponse<T>> RunWorkFlowProcessAsync();
    }
}
