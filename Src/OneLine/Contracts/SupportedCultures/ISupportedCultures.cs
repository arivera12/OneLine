using System.Globalization;

namespace OneLine.Contracts
{
    public interface ISupportedCultures
    {
        IEnumerable<CultureInfo> Cultures { get; set; }
    }
}
