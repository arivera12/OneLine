using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ISearchExtraParams<TSearchExtraParams>
    {
        TSearchExtraParams SearchExtraParams { get; set; }
    }
}
