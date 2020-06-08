
using OneLine.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace OneLine.Models
{
    public class ResponseResult<T> : IResponseResult<T>
    {
        /// <summary>
        /// The response from the server
        /// </summary>
        public virtual T Response { get; set; }
        /// <summary>
        /// Determines where the server response succeed 
        /// </summary>
        public virtual bool Succeed { get; set; }
        /// <summary>
        /// Determines when the request was successfully or not
        /// </summary>
        public virtual bool HasException { get; set; }
        /// <summary>
        /// The exception when an error ocurred on the request
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// The HTTP response message including the status code and data. 
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; set; }
        public ResponseResult(T response = default, Exception exception = null, HttpResponseMessage httpResponseMessage = null)
        {
            Response = response;
            Exception = exception;
            HasException = Exception.IsNotNull();
            Succeed = (httpResponseMessage.IsNotNull() && httpResponseMessage.IsSuccessStatusCode) && !HasException;
            HttpResponseMessage = httpResponseMessage;
        }
    }
}
