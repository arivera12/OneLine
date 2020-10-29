using System;
using System.Net.Http;

namespace OneLine.Models
{
    /// <summary>
    /// This interface defines a holder and evaluator of a response result of a <see cref="HttpClient"/> and the <see cref="System.Net.Http.HttpResponseMessage"/>
    /// </summary>
    /// <typeparam name="T">The type of the response data</typeparam>
    public interface IResponseResult<T>
    {
        /// <summary>
        /// The response from the server
        /// </summary>
        T Response { get; set; }
        /// <summary>
        /// Determines where the server response succeed 
        /// </summary>
        bool Succeed { get; set; }
        /// <summary>
        /// Determines when the request was successfully or not
        /// </summary>
        bool HasException { get; set; }
        /// <summary>
        /// The exception when an error ocurred on the request
        /// </summary>
        Exception Exception { get; set; }
        /// <summary>
        /// The HTTP response message including the status code and data. 
        /// </summary>
        HttpResponseMessage HttpResponseMessage { get; set; }
    }
}
