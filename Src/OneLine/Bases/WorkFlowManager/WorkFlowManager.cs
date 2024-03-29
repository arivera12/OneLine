﻿using OneLine.Contracts;
using OneLine.Extensions;
using OneLine.Models;

namespace OneLine.Bases
{
    public class WorkFlowManager<T, TState> : IWorkFlowManager<T, TState>
    {
        public WorkFlowManager()
        {
        }
        /// <inheritdoc/>
        public Func<T, string> StatePropertyName { get; set; }
        /// <inheritdoc/>
        public T Record { get; set; }
        /// <inheritdoc/>
        public IEnumerable<IWorkFlowStateProcess<T, TState>> WorkFlowStateProcesses { get; set; }
        /// <inheritdoc/>
        public async ValueTask<IApiResponse<T>> RunWorkFlowProcessAsync()
        {
            if (WorkFlowStateProcesses.IsNull() || !WorkFlowStateProcesses.Any())
                throw new ArgumentNullException(nameof(WorkFlowStateProcesses));
            if (Record.IsNull())
                throw new ArgumentNullException(nameof(Record));
            if (StatePropertyName.IsNull())
                throw new ArgumentNullException(nameof(StatePropertyName));
            var currentStateProperty = typeof(T).GetProperties().FirstOrDefault(w => w.PropertyType == typeof(TState) && w.Name == StatePropertyName(Record)).GetValue(Record);
            if (currentStateProperty.IsNull())
                throw new ArgumentNullException(nameof(currentStateProperty));
            var recordCurrentState = (TState)currentStateProperty;
            var nextAvailableWorkFlowStateProcessesFromCurrentState = WorkFlowStateProcesses.Where(w => w.CurrentState.Equals(recordCurrentState));
            if (!nextAvailableWorkFlowStateProcessesFromCurrentState.Any())
                return new ApiResponse<T>(Enums.ApiResponseStatus.Succeeded, Record, "ThereAreNoStepsAvailableAfterTheCurrentState");
            IApiResponse<T> response = default;
            foreach (var nextWorkFlowNextState in nextAvailableWorkFlowStateProcessesFromCurrentState)
            {
                var result = await nextWorkFlowNextState.ProceedNextStateProcessWhen();
                if (result)
                {
                    response = await nextWorkFlowNextState.ProceedNextStateProcess();
                    break;
                }
            }
            return response;
        }
    }
}
