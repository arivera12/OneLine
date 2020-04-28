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
        ObservableRangeCollection<T> SelectedRecords { get; set; }
        long MinimunRecordSelections { get; set; }
        long MaximumRecordSelections { get; set; }
        bool MinimunRecordSelectionsReached { get; set; }
        bool MaximumRecordSelectionsReached { get; set; }
        Action<T> OnSelectedRecord { get; set; }
        Action<IEnumerable<T>, bool, bool> OnSelectedRecords { get; set; }
        Action<bool> OnMinimunRecordSelectionsReached { get; set; }
        Action<bool> OnMaximumRecordSelectionsReached { get; set; }
        Task SelectRecord(T selectedRecord);
    }
}
