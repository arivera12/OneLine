using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ICollectionFilterableSortable<T>
    {
        Task FilterAndSort();
        Task FilterAndSort(string sortBy, bool descending);
        Task FilterAndSort(string sortBy, bool descending, Func<T, bool> filterPredicate);
        Func<T, bool> FilterPredicate { get; set; }
        string FilterSortBy { get; set; }
        bool FilterDescending { get; set; }
        ObservableRangeCollection<T> RecordsFilteredSorted { get; set; }
    }
}
