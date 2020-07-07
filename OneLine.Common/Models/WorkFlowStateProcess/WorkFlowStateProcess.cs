using System.Threading.Tasks;

namespace OneLine.Models
{
    public class WorkFlowStateProcess<T, TState> : IWorkFlowStateProcess<T, TState>
    {
        public virtual T Record { get; set; }
        public virtual TState CurrentState { get; set; }
        public virtual TState NextState { get; set; }
        public virtual Task<bool> ProceedNextStateProcessWhen()
        {
            return default;
        }
        public virtual Task<IApiResponse<T>> ProceedNextStateProcess()
        {
            return default;
        }
    }
}
