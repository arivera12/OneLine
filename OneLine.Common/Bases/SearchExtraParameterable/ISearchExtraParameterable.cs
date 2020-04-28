using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ISearchExtraParameterable<TSearchExtraParams>
    {
        TSearchExtraParams SearchExtraParams { get; set; }
    }
}
