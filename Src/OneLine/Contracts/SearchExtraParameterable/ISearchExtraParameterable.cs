using System.Threading.Tasks;

namespace OneLine.Contracts
{
    /// <summary>
    /// Defines a property which is capable to be an extra parameter fo seach purpose
    /// </summary>
    /// <typeparam name="TSearchExtraParams"></typeparam>
    public interface ISearchExtraParameterable<TSearchExtraParams>
    {
        /// <summary>
        /// The search extra paremeters
        /// </summary>
        TSearchExtraParams SearchExtraParams { get; set; }
    }
}
