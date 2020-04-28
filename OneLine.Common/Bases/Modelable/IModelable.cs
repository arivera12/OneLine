using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OneLine.Bases
{
    public interface IModelable<T, TIdentifier>
    {
        TIdentifier Identifier { get; set; }
        IEnumerable<TIdentifier> Identifiers { get; set; }
        T Record { get; set; }
        ObservableRangeCollection<T> Records { get; set; }
    }
}