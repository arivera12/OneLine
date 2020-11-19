using System.Net.Http;

namespace OneLine.Contracts
{
    /// <summary>
    /// Defines a http service using the <see cref="HttpClient"/>
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// The api url path
        /// </summary>
        string Api { get; set; }
        /// <summary>
        /// The http client
        /// </summary>
        HttpClient HttpClient { get; set; }
    }
}
