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
        Action<Func<T, bool>> FilterPredicateChanged { get; set; }
        string FilterSortBy { get; set; }
        Action<string> FilterSortByChanged { get; set; }
        bool FilterDescending { get; set; }
        Action<bool> FilterDescendingChanged { get; set; }
        ObservableRangeCollection<T> RecordsFilteredSorted { get; set; }
        Action<ObservableRangeCollection<T>> RecordsFilteredSortedChanged { get; set; }
    }
}
