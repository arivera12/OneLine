using OneLine.Models;
using System;
using System.Collections.Generic;

namespace OneLine.Contracts
{
    /// <summary>
    /// This interface is a definition of actions based on the state and response result paged of the api 
    /// </summary>
    /// <typeparam name="T">The api response paged type</typeparam>
    public interface IApiResponseablePageable<T>
    {
        /// <summary>
        /// The response result paged
        /// </summary>
        IResponseResult<ApiResponse<Paged<IEnumerable<T>>>> ResponsePaged { get; set; }
        /// <summary>
        /// The response result paged changed action
        /// </summary>
        Action<IResponseResult<ApiResponse<Paged<IEnumerable<T>>>>> ResponsePagedChanged { get; set; }
    }
}
