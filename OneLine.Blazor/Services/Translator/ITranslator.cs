using JsonLanguageLocalizerNet;
using System.Threading.Tasks;

namespace OneLine.Blazor.Services
{
    public interface ITranslator
    {
        Task<JsonLanguageLocalizerService> Translate(string applicationLocale);
        Task<string> GetCurrentApplicationLocale();
    }
}
