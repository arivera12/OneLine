using FluentValidation;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OneLine.Bases
{
    public abstract partial class FormBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IForm<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        public FormBase()
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
        }
        public FormBase(FormState formState)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
        }
        public FormBase(FormMode formMode)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>(); ;
            FormMode = formMode;
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(T record)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
        }
        public FormBase(IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.AddRange(records);
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
        }
        public FormBase(FormState formState, FormMode formMode)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            FormMode = formMode;
        }
        public FormBase(FormState formState, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(FormState formState, IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(FormState formState, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
        }
        public FormBase(FormState formState, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
        }
        public FormBase(FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormMode formMode, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormMode = formMode;
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(FormMode formMode, IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormMode = formMode;
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(FormMode formMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormMode = formMode;
        }
        public FormBase(FormMode formMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormMode = formMode;
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
    }
}
