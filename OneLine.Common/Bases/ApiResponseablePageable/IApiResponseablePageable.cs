using OneLine.Models;
using System;
using System.Collections.Generic;

namespace OneLine.Bases
{
    /// <summary>
    /// This interface is a definition of actions based on the state and response result paged of the api 
    /// </summary>
    /// <typeparam name="T">The api response paged type</typeparam>
    public interface IApiResponseablePageable<T>
    {
        IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>> ResponsePaged { get; set; }
        Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> ResponsePagedChanged { get; set; }
    }
}
