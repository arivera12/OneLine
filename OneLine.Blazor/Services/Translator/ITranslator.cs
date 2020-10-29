using JsonLanguageLocalizerNet;
using System.Threading.Tasks;

namespace OneLine.Blazor.Services
{
    /// <summary>
    /// A translator service methods
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// Translates the current application
        /// </summary>
        /// <param name="applicationLocale"></param>
        /// <returns></returns>
        Task<JsonLanguageLocalizerService> Translate(string applicationLocale);
        /// <summary>
        /// Gets the current application locale
        /// </summary>
        /// <returns></returns>
        Task<string> GetCurrentApplicationLocale();
    }
}
