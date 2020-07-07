using System.Threading.Tasks;

namespace OneLine.Models
{
    public interface IWorkFlowStateProcess<T, TState>
    {
        T Record { get; set; }
        TState CurrentState { get; set; }
        TState NextState { get; set; }
        Task<bool> ProceedNextStateProcessWhen();
        Task<IApiResponse<T>> ProceedNextStateProcess();
    }
}
