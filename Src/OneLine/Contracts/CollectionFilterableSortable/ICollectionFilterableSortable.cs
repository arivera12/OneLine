using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    /// <summary>
    /// The collection is filterable and sortable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionFilterableSortable<T>
    {
        /// <summary>
        /// Filter and sort method
        /// </summary>
        /// <returns></returns>
        Task FilterAndSort();
        /// <summary>
        /// Filter and sort method by a property name and descending or ascending
        /// </summary>
        /// <param name="sortBy">The property name to be sorted by</param>
        /// <param name="descending">Descending or ascending</param>
        /// <returns></returns>
        Task FilterAndSort(string sortBy, bool descending);
        /// <summary>
        /// Filter and sort method by a property name and descending or ascending with a filter predicate
        /// </summary>
        /// <param name="sortBy">The property name to be sorted by</param>
        /// <param name="descending">Descending or ascending</param>
        /// <param name="filterPredicate">The predicate filter</param>
        /// <returns></returns>
        Task FilterAndSort(string sortBy, bool descending, Func<T, bool> filterPredicate);
        /// <summary>
        /// The filter predicate
        /// </summary>
        Func<T, bool> FilterPredicate { get; set; }
        /// <summary>
        /// The filter predicate changed action
        /// </summary>
        Action<Func<T, bool>> FilterPredicateChanged { get; set; }
        /// <summary>
        /// The sort by filter by property name
        /// </summary>
        string FilterSortBy { get; set; }
        /// <summary>
        /// The filter sort by property name changed action
        /// </summary>
        Action<string> FilterSortByChanged { get; set; }
        /// <summary>
        /// The sorting filter 
        /// </summary>
        bool FilterDescending { get; set; }
        /// <summary>
        /// The sorting filter changed action
        /// </summary>
        Action<bool> FilterDescendingChanged { get; set; }
        /// <summary>
        /// The records filtered and sorted result
        /// </summary>
        ObservableRangeCollection<T> RecordsFilteredSorted { get; set; }
        /// <summary>
        /// The records filtered and sorted result changed action
        /// </summary>
        Action<ObservableRangeCollection<T>> RecordsFilteredSortedChanged { get; set; }
    }
}
