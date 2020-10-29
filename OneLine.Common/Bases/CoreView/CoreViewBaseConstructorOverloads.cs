using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OneLine.Bases
{
    /// <summary>
    /// Base core view implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TIdentifier"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="THttpService"></typeparam>
    public abstract partial class CoreViewBase<T, TIdentifier, TId, THttpService> :
        ICoreView<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        public CoreViewBase()
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            RecordsFilteredSorted = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            Paging = new Paging();
        }
        public CoreViewBase(object[] searchExtraParams)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            RecordsFilteredSorted = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            Paging = new Paging();
            SearchExtraParams = searchExtraParams;
        }
        public CoreViewBase(SearchPaging searchPaging)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            RecordsFilteredSorted = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            SearchPaging = searchPaging;
            Paging = new Paging();
        }
        public CoreViewBase(Paging paging)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            RecordsFilteredSorted = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            Paging = paging;
        }
        public CoreViewBase(SearchPaging searchPaging, object[] searchExtraParams)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            RecordsFilteredSorted = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            SearchPaging = searchPaging;
            SearchExtraParams = searchExtraParams;
        }
        public CoreViewBase(Paging paging, object[] searchExtraParams)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            RecordsFilteredSorted = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            Paging = paging;
            SearchExtraParams = searchExtraParams;
        }
        public CoreViewBase(TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            RecordsFilteredSorted = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            Paging = new Paging();
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public CoreViewBase(TIdentifier identifier, bool autoLoad = false, bool initialAutoSearch = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            RecordsFilteredSorted = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            Paging = new Paging();
            AutoLoad = autoLoad;
            InitialAutoSearch = initialAutoSearch;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
            if (InitialAutoSearch)
            {
                _ = new Action(async () => await Search());
            }
        }
        public CoreViewBase(IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            RecordsFilteredSorted = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            Paging = new Paging();
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public CoreViewBase(IEnumerable<TIdentifier> identifiers, bool autoLoad = false, bool initialAutoSearch = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            RecordsFilteredSorted = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            Paging = new Paging();
            AutoLoad = autoLoad;
            InitialAutoSearch = initialAutoSearch;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
            if (InitialAutoSearch)
            {
                _ = new Action(async () => await Search());
            }
        }
        public CoreViewBase(T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            RecordsFilteredSorted = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            Paging = new Paging();
        }
        public CoreViewBase(IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            Records.AddRange(records);
            RecordsFilteredSorted = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            RecordsFilteredSorted.AddRange(records);
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            Paging = new Paging();
        }
        public CoreViewBase(FormState formState)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
        }
        public CoreViewBase(FormMode formMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormMode = formMode;
        }
        public CoreViewBase(CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public CoreViewBase(FormState formState, FormMode formMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
        }
        public CoreViewBase(FormState formState, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public CoreViewBase(FormState formState, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public CoreViewBase(FormState formState, IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public CoreViewBase(FormState formState, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
        }
        public CoreViewBase(FormState formState, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            FormState = formState;
        }
        public CoreViewBase(FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public CoreViewBase(FormMode formMode, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormMode = formMode;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public CoreViewBase(FormMode formMode, IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormMode = formMode;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public CoreViewBase(FormMode formMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormMode = formMode;
        }
        public CoreViewBase(FormMode formMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            FormMode = formMode;
        }
        public CoreViewBase(CollectionAppendReplaceMode collectionAppendReplaceMode, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public CoreViewBase(CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public CoreViewBase(CollectionAppendReplaceMode collectionAppendReplaceMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public CoreViewBase(CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public CoreViewBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode)
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
        public CoreViewBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public CoreViewBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public CoreViewBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public CoreViewBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
    }
}
