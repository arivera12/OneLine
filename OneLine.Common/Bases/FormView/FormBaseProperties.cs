using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OneLine.Bases
{
    public abstract partial class FormViewBase<T, TIdentifier, TId, THttpService> :
        IFormView<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        public virtual T Record { get; set; }
        public virtual ObservableRangeCollection<T> Records { get; set; }
        public virtual bool AllowDuplicates { get; set; }
        public virtual bool AutoLoad { get; set; }
        public virtual TIdentifier Identifier { get; set; }
        public virtual IEnumerable<TIdentifier> Identifiers { get; set; }
        public virtual CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        public virtual IValidator Validator { get; set; }
        public virtual ValidationResult ValidationResult { get; set; }
        public virtual bool IsValidModelState { get; set; }
        public virtual THttpService HttpService { get; set; }
        public virtual IConfiguration Configuration { get; set; }
        public virtual FormState FormState { get; set; }
        public virtual FormMode FormMode { get; set; }
        public virtual IResponseResult<ApiResponse<T>> Response { get; set; }
        public virtual IResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        public virtual Action<TIdentifier> IdentifierChanged { get; set; }
        public virtual Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        public virtual Action<T> RecordChanged { get; set; }
        public virtual Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        public virtual Action<ValidationResult> ValidationResultChanged { get; set; }
        public virtual Action<bool> IsValidModelStateChanged { get; set; }
        public virtual Action<FormState> FormStateChanged { get; set; }
        public virtual Action<FormMode> FormModeChanged { get; set; }
        public virtual Action<IResponseResult<ApiResponse<T>>> ResponseChanged { get; set; }
        public virtual Action<IResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
        public virtual Action OnBeforeReset { get; set; }
        public virtual Action OnAfterReset { get; set; }
        public virtual Action OnBeforeCancel { get; set; }
        public virtual Action OnAfterCancel { get; set; }
        public virtual Action OnBeforeSave { get; set; }
        public virtual Action OnAfterSave { get; set; }
        public virtual Action OnBeforeDelete { get; set; }
        public virtual Action OnAfterDelete { get; set; }
        public virtual Action OnBeforeValidate { get; set; }
        public virtual Action OnAfterValidate { get; set; }
    }
}
