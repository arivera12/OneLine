using OneLine.Extensions;
using System;
using System.Net.Http;

namespace OneLine.Models
{
    /// <summary>
    /// This class implements a holder and evaluator of a response result of a <see cref="HttpClient"/> and the <see cref="System.Net.Http.HttpResponseMessage"/>
    /// </summary>
    /// <typeparam name="T">The type of the response data</typeparam>
    public class ResponseResult<T> : IResponseResult<T>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public T Response { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Succeed { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool HasException { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public ResponseResult() { }
        /// <summary>
        /// The response result main constructor
        /// </summary>
        /// <param name="response">The response</param>
        /// <param name="exception">The exception</param>
        /// <param name="httpResponseMessage">The httpResponseMessage</param>
        public ResponseResult(T response = default, Exception exception = null, HttpResponseMessage httpResponseMessage = null)
        {
            Response = response;
            Exception = exception;
            HasException = Exception.IsNotNull();
            Succeed = httpResponseMessage.IsNotNull() && httpResponseMessage.IsSuccessStatusCode && !HasException;
            HttpResponseMessage = httpResponseMessage;
        }
    }
}
