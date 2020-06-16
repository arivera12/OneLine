using OneLine.Models;
using System;
using System.Collections.Generic;

namespace OneLine.Bases
{
    /// <summary>
    /// This interface is a definition of actions based on the state and response result collection of the api
    /// </summary>
    /// <typeparam name="T">The type of the api response collection</typeparam>
    public interface IApiResponseableCollectionable<T>
    {
        IResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        Action<IResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
    }
}
