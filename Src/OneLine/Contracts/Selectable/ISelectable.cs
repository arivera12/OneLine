using OneLine.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    /// <summary>
    /// Defines properties and methods for selection mode, options and actions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISelectable<T>
    {
        /// <summary>
        /// The record selection mode <see cref="Enums.RecordsSelectionMode"/>
        /// </summary>
        RecordsSelectionMode RecordsSelectionMode { get; set; }
        /// <summary>
        /// The context selected record
        /// </summary>
        T SelectedRecord { get; set; }
        /// <summary>
        /// The context selecte records
        /// </summary>
        System.Collections.ObjectModel.ObservableRangeCollection<T> SelectedRecords { get; set; }
        /// <summary>
        /// The minimun allowed o required records selections
        /// </summary>
        long MinimumRecordsSelections { get; set; }
        /// <summary>
        /// The maximun allowed o required records selections
        /// </summary>
        long MaximumRecordsSelections { get; set; }
        /// <summary>
        /// The minimun records selection indicator
        /// </summary>
        bool MinimumRecordsSelectionsReached { get; set; }
        /// <summary>
        /// The maximun records selection indicator
        /// </summary>
        bool MaximumRecordsSelectionsReached { get; set; }
        /// <summary>
        /// The select record method
        /// </summary>
        /// <param name="selectedRecord"></param>
        /// <returns></returns>
        Task SelectRecord(T selectedRecord);
        /// <summary>
        /// The select records method
        /// </summary>
        /// <param name="selectedRecords"></param>
        /// <returns></returns>
        Task SelectRecords(IEnumerable<T> selectedRecords);
        /// <summary>
        /// The before selected record action callback. This action callback should be chained with <see cref="SelectRecord(T)"/> or <see cref="SelectRecords(IEnumerable{T})"/>
        /// </summary>
        Action BeforeSelectedRecord { get; set; }
        /// <summary>
        /// The after selected record action callback. This action callback should be called from <see cref="SelectRecord(T)"/> or <see cref="SelectRecords(IEnumerable{T})"/>
        /// </summary>
        Action AfterSelectedRecord { get; set; }
        /// <summary>
        /// The selected record changed action callback
        /// </summary>
        Action<T> SelectedRecordChanged { get; set; }
        /// <summary>
        /// The selected records changed action callback
        /// </summary>
        Action<ObservableRangeCollection<T>> SelectedRecordsChanged { get; set; }
        /// <summary>
        /// The minimun records selection reached indicator action callback
        /// </summary>
        Action<bool> MinimumRecordsSelectionsReachedChanged { get; set; }
        /// <summary>
        /// The maximum records selection reached indicator action callback
        /// </summary>
        Action<bool> MaximumRecordsSelectionsReachedChanged { get; set; }
    }
}
