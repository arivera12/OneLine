using System.Resources;
using System.Threading.Tasks;

namespace OneLine.Services
{
    /// <summary>
    /// A translator service using the resource manager
    /// </summary>
    public interface IResourceManagerLocalizer
    {
        //
        // Summary:
        //     Gets a resource value.
        //
        // Parameters:
        //   key:
        //     The resource key.
        //
        // Returns:
        //     The resource value.
        string this[string key] { get; }
        /// <summary>
        /// Gets the current application resource manager
        /// </summary>
        ResourceManager ResourceManager { get; set; }
        /// <summary>
        /// Gets the current application locale from storage
        /// </summary>
        /// <returns></returns>
        Task<string> GetApplicationLocale();
        /// <summary>
        /// Sets the current application locale in the storage and sets current thread culture
        /// </summary>
        /// <returns></returns>
        Task SetApplicationLocale(string applicationLocale);
        /// <summary>
        /// Sets the <see cref="System.Threading.Thread.CurrentThread.CurrentCulture"/> and 
        /// <see cref="System.Threading.Thread.CurrentThread.CurrentUICulture"/> with the current application locale
        /// </summary>
        /// <returns></returns>
        Task SetCurrentThreadCulture();
        /// <summary>
        /// Sets the <see cref="System.Threading.Thread.CurrentThread.CurrentCulture"/> and 
        /// <see cref="System.Threading.Thread.CurrentThread.CurrentUICulture"/> with the specified application locale
        /// </summary>
        /// <param name="applicationLocale"></param>
        /// <returns></returns>
        Task SetCurrentThreadCulture(string applicationLocale);
    }
}
