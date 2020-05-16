using OneLine.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ISelectable<T>
    {
        RecordsSelectionMode RecordsSelectionMode { get; set; }
        T SelectedRecord { get; set; }
        ObservableRangeCollection<T> SelectedRecords { get; set; }
        long MinimunRecordsSelections { get; set; }
        long MaximumRecordsSelections { get; set; }
        bool MinimunRecordsSelectionsReached { get; set; }
        bool MaximumRecordsSelectionsReached { get; set; }
        Task SelectRecord(T selectedRecord);
        Action<Action<T>> BeforeSelectedRecord { get; set; }
        Action AfterSelectedRecord { get; set; }
        Action<T> SelectedRecordChanged { get; set; }
        Action<IEnumerable<T>, bool, bool> SelectedRecordsChanged { get; set; }
        Action<bool> MinimunRecordsSelectionsReachedChanged { get; set; }
        Action<bool> MaximumRecordsSelectionsReachedChanged { get; set; }
    }
}
