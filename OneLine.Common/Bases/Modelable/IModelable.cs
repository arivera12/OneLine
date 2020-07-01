using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OneLine.Bases
{
    public interface IModelable<T, TIdentifier>
    {
        TIdentifier Identifier { get; set; }
        Action<TIdentifier> IdentifierChanged { get; set; }
        IEnumerable<TIdentifier> Identifiers { get; set; }
        Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        T Record { get; set; }
        Action<T> RecordChanged { get; set; }
        ObservableRangeCollection<T> Records { get; set; }
        Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        bool AllowDuplicates { get; set; }
    }
}