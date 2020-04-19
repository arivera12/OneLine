using OneLine.Models;
using System;
using System.Collections.Generic;

namespace OneLine.Bases
{
    public interface IApiResponsePaged<T>
    {
        IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>> ResponsePaged { get; set; }
        Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePaged { get; set; }
        Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePagedSucceeded { get; set; }
        Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePagedException { get; set; }
        Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePagedFailed { get; set; }
    }
}
