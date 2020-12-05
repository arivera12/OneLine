using System.Collections.Generic;
using System.Globalization;

namespace OneLine.Services
{
    public interface ISupportedCultures
    {
        IEnumerable<CultureInfo> Cultures { get; }
    }
}
