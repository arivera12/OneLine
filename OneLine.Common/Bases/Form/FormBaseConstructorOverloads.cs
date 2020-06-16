using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OneLine.Bases
{
    public abstract partial class FormBase<T, TIdentifier, TId, THttpService> :
        IForm<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        public FormBase()
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
        }
        public FormBase(FormState formState)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            FormState = formState;
        }
        public FormBase(FormMode formMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            FormMode = formMode;
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
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
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
        }
        public FormBase(IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.AddRange(records);
            HttpService = new THttpService();
        }
        public FormBase(FormState formState, FormMode formMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
        }
        public FormBase(FormState formState, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            FormState = formState;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
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
            FormState = formState;
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(FormState formState, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            FormState = formState;
        }
        public FormBase(FormState formState, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            FormState = formState;
        }
        public FormBase(FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormMode formMode, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
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
            FormMode = formMode;
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(FormMode formMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            FormMode = formMode;
        }
        public FormBase(FormMode formMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            FormMode = formMode;
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
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
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            if (autoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
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
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
    }
}
