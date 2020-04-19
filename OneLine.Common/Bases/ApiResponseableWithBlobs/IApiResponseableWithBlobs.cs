using OneLine.Models;
using System;
using System.Collections.Generic;

namespace OneLine.Bases
{
    public interface IApiResponseableWithBlobs<T, TUserBlobs>
    {
        IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> ResponseAddWithBlobs { get; set; }
        Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobs { get; set; }
        Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobsSucceeded { get; set; }
        Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobsException { get; set; }
        Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobsFailed { get; set; }
        IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>> ResponseUpdateWithBlobs { get; set; }
        Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobs { get; set; }
        Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobsSucceeded { get; set; }
        Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobsException { get; set; }
        Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobsFailed { get; set; }
    }
}
