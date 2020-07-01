using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OneLine.Bases
{
    public abstract partial class DataViewBase<T, TIdentifier, TId, THttpService> :
        IDataView<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        public DataViewBase()
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
        public DataViewBase(object[] searchExtraParams)
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
        public DataViewBase(SearchPaging searchPaging)
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
        public DataViewBase(Paging paging)
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
        public DataViewBase(SearchPaging searchPaging, object[] searchExtraParams)
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
        public DataViewBase(Paging paging, object[] searchExtraParams)
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
        public DataViewBase(TIdentifier identifier, bool autoLoad = false)
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
        public DataViewBase(TIdentifier identifier, bool autoLoad = false, bool initialAutoSearch = false)
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
        public DataViewBase(IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
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
        public DataViewBase(IEnumerable<TIdentifier> identifiers, bool autoLoad = false, bool initialAutoSearch = false)
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
            if(InitialAutoSearch)
            {
                _ = new Action(async () => await Search());
            }
        }
        public DataViewBase(T record)
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
        public DataViewBase(IEnumerable<T> records)
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
    }
}