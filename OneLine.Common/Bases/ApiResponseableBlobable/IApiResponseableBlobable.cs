using OneLine.Models;
using System;
using System.Collections.Generic;

namespace OneLine.Bases
{
    /// <summary>
    /// This interface is a definition of actions based on the state and response result of the api with blobs
    /// </summary>
    /// <typeparam name="T">The api response type</typeparam>
    public interface IApiResponseableBlobable<T, TUserBlobs>
    {
        IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> ResponseAddWithBlobs { get; set; }
        Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobs { get; set; }
        IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> ResponseAddCollectionWithBlobs { get; set; }
        Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> OnResponseAddCollectionWithBlobs { get; set; }
        Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobs { get; set; }
        IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>> ResponseUpdateCollectionWithBlobs { get; set; }
        Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateCollectionWithBlobs { get; set; }
    }
}