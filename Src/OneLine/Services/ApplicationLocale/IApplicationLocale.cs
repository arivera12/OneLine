using System.Threading.Tasks;

namespace OneLine.Services
{
    /// <summary>
    /// A translator service using the resource manager
    /// </summary>
    public interface IApplicationLocale
    {
        /// <summary>
        /// Gets the current application locale from storage
        /// </summary>
        /// <returns></returns>
        Task<string> GetApplicationLocale();
        /// <summary>
        /// Sets the current application locale in the storage
        /// </summary>
        /// <returns></returns>
        Task SetApplicationLocale(string applicationLocale);
    }
}
