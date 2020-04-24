using FluentValidation;
using Microsoft.Extensions.Configuration;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class DataViewBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IDataView<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : new()
        where TIdentifier : IIdentifier<TId>, new()
        where TId : class
        where THttpService : HttpBaseCrudExtendedService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : IBlobData
        where TBlobValidator : IValidator, new()
        where TUserBlobs : IUserBlobs
    {
        public virtual TIdentifier Identifier { get; set; } = new TIdentifier();
        public virtual T Record { get; set; } = new T();
        public virtual IEnumerable<T> Records { get; set; }
        public virtual object SearchExtraParams { get; set; }
        public virtual IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>> ResponsePaged { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePaged { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePagedSucceeded { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePagedException { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePagedFailed { get; set; }
        public virtual THttpService HttpService { get; set; } = new THttpService(); 
        public virtual IConfiguration Configuration { get; set; }
        public virtual ISearchPaging SearchPaging { get; set; }
        public virtual RecordsSelectionMode RecordsSelectionMode { get; set; }
        public virtual IList<T> SelectedRecords { get; set; }
        public virtual long MinimunRecordSelections { get; set; }
        public virtual long MaximumRecordSelections { get; set; }
        public virtual bool MinimunRecordSelectionsReached { get; set; }
        public virtual bool MaximumRecordSelectionsReached { get; set; }
        public virtual Action<T> OnSelectedRecord { get; set; }
        public virtual Action<IEnumerable<T>, bool, bool> OnSelectedRecords { get; set; }
        public virtual Action<bool> OnMinimunRecordSelectionsReached { get; set; }
        public virtual Action<bool> OnMaximumRecordSelectionsReached { get; set; }
        public virtual async Task Search()
        {
            ResponsePaged = await HttpService.Search<T>(SearchPaging, SearchExtraParams);
            if (ResponsePaged.Succeed && ResponsePaged.Response.Status.Succeeded())
            {
                Records = ResponsePaged.Response.Data.Data;
                OnResponsePagedSucceeded?.Invoke(ResponsePaged);
            }
            else if (ResponsePaged.HasException)
            {
                OnResponsePagedException?.Invoke(ResponsePaged);
            }
            else
            {
                OnResponsePagedFailed?.Invoke(ResponsePaged);
            }
            OnResponsePaged?.Invoke(ResponsePaged);
        }
        public virtual async Task PagingFilterChange(IPaging paging)
        {
            SearchPaging.AutoMap(paging);
            await Search();
        }
        public virtual Task SelectRecord(T selectedRecord)
        {
            if (RecordsSelectionMode == RecordsSelectionMode.Single)
            {
                OnSelectedRecord?.Invoke(selectedRecord);
            }
            else if (RecordsSelectionMode == RecordsSelectionMode.Multiple)
            {
                if (SelectedRecords.Contains(selectedRecord))
                {
                    SelectedRecords.Remove(selectedRecord);
                }
                else if (MaximumRecordSelections <= 0 || (MaximumRecordSelections > 0 && SelectedRecords.Count < MaximumRecordSelections))
                {
                    SelectedRecords.Add(selectedRecord);
                }
                MinimunRecordSelectionsReached = SelectedRecords.Count >= MinimunRecordSelections;
                OnMinimunRecordSelectionsReached?.Invoke(MinimunRecordSelectionsReached);
                MaximumRecordSelectionsReached = SelectedRecords.Count >= MaximumRecordSelections;
                OnMaximumRecordSelectionsReached?.Invoke(MaximumRecordSelectionsReached);
                OnSelectedRecords?.Invoke(SelectedRecords, MinimunRecordSelectionsReached, MaximumRecordSelectionsReached);
            }
            return Task.CompletedTask;
        }
    }
}