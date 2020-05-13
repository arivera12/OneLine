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
        ResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        Action<ResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
    }
}
