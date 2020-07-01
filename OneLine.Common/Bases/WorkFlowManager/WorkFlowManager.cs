using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class WorkFlowManager<T, TState> : IWorkFlowManager<T, TState>
    {
        public IEnumerable<IWorkFlowStateProcess<T, TState>> WorkFlowStateProcesses { get; set; }
        public async ValueTask<IApiResponse<T>> RunWorkFlowProcessAsync()
        {
            if (WorkFlowStateProcesses.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(WorkFlowStateProcesses));

            var countOfTState = typeof(T).GetProperties().Where(w => w.PropertyType == typeof(TState)).Count();

            if (countOfTState == 0)
                throw new Exception("The T record doesn't contain any property with the TState specified. Please make sure that only one property is TState exist in the T record");
            if (countOfTState >= 2)
                throw new Exception($"We found {countOfTState} of TState in the T record. Please make sure that only one property is TState exist in the T record");

            var record = WorkFlowStateProcesses.FirstOrDefault().Record;

            var currentState = (TState)typeof(T).GetProperties().FirstOrDefault(w => w.PropertyType == typeof(TState)).GetValue(record);

            var currentworkFlowStateProcesses = WorkFlowStateProcesses.Where(w => w.CurrentState.Equals(currentState));

            foreach (var currentWorkFlowNextState in currentworkFlowStateProcesses)
            {
                var result = await currentWorkFlowNextState.ProceedNextStateProcessWhen();
                if (result)
                {
                    record = await currentWorkFlowNextState.ProceedNextStateProcess();
                    break;
                }
            }

            return new ApiResponse<T>(Enums.ApiResponseStatus.Succeeded, record);
        }
    }
}
