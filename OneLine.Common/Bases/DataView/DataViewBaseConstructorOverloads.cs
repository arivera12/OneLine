using FluentValidation;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OneLine.Bases
{
    public abstract partial class DataViewBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IDataView<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : class, IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        public DataViewBase()
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
        }
        public DataViewBase(object searchExtraParams)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            SearchExtraParams = searchExtraParams;
        }
        public DataViewBase(SearchPaging searchPaging)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = searchPaging;
        }
        public DataViewBase(SearchPaging searchPaging, object searchExtraParams)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = searchPaging;
            SearchExtraParams = searchExtraParams;
        }
        public DataViewBase(TIdentifier identifier)
        {
            Identifier = identifier;
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            _ = new Action(async () => await Load());
        }
        public DataViewBase(IEnumerable<TIdentifier> identifiers)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            _ = new Action(async () => await Load());
        }
        public DataViewBase(T record)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
        }
        public DataViewBase(IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.AddRange(records);
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            RecordsFilteredSorted.AddRange(records);
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
        }
    }
}