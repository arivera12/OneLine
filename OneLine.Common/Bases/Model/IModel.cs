using System.Collections.Generic;

namespace OneLine.Bases
{
    public interface IModel<T, TIdentifier>
    {
        TIdentifier Identifier { get; set; }
        T Record { get; set; }
        IEnumerable<T> Records { get; set; }
    }
}