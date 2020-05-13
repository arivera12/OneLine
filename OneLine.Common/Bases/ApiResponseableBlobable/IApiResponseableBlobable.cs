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
        ResponseResult<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> ResponseAddWithBlobs { get; set; }
        Action<ResponseResult<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> ResponseAddWithBlobsChanged { get; set; }
        ResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> ResponseAddCollectionWithBlobs { get; set; }
        Action<ResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> ResponseAddCollectionWithBlobsChanged { get; set; }
        ResponseResult<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>> ResponseUpdateWithBlobs { get; set; }
        Action<ResponseResult<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> ResponseUpdateWithBlobsChanged { get; set; }
        ResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>> ResponseUpdateCollectionWithBlobs { get; set; }
        Action<ResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> ResponseUpdateCollectionWithBlobsChanged { get; set; }
    }
}