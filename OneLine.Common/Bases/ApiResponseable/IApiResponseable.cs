using OneLine.Models;
using System;

namespace OneLine.Bases
{
    public interface IApiResponseable<T>
    {
        IResponseResult<IApiResponse<T>> Response { get; set; }
        Action<IResponseResult<IApiResponse<T>>> OnResponse { get; set; }
        Action<IResponseResult<IApiResponse<T>>> OnResponseSucceeded { get; set; }
        Action<IResponseResult<IApiResponse<T>>> OnResponseException { get; set; }
        Action<IResponseResult<IApiResponse<T>>> OnResponseFailed { get; set; }
    }
}
