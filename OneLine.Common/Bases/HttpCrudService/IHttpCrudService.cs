using FluentValidation;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IHttpCrudService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs> : IHttpService
    {
        string ControllerName { get; set; }
        string AddMethod { get; set; }
        string AddRangeMethod { get; set; }
        string UpdateMethod { get; set; }
        string UpdateRangeMethod { get; set; }
        string DeleteMethod { get; set; }
        string DeleteRangeMethod { get; set; }
        string GetOneMethod { get; set; }
        string GetRangeMethod { get; set; }
        string SearchMethod { get; set; }
        TBlobValidator BlobValidator { get; set; }
        Task<IResponseResult<IApiResponse<TResponse>>> Add<TResponse>(T record);
        Task<IResponseResult<IApiResponse<TResponse>>> Add<TResponse>(T record, IValidator validator);
        Task<IResponseResult<IApiResponse<TResponse>>> AddRange<TResponse>(IEnumerable<T> records, IValidator validator);
        Task<IResponseResult<IApiResponse<TResponse>>> AddRange<TResponse>(IEnumerable<T> records);
        Task<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> Add(T record, IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> Add(T record, IValidator validator, IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> AddRange(IEnumerable<T> records, IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> AddRange(IEnumerable<T> records, IValidator validator, IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<TResponse>>> Update<TResponse>(T record);
        Task<IResponseResult<IApiResponse<TResponse>>> Update<TResponse>(T record, IValidator validator);
        Task<IResponseResult<IApiResponse<TResponse>>> UpdateRange<TResponse>(IEnumerable<T> records);
        Task<IResponseResult<IApiResponse<TResponse>>> UpdateRange<TResponse>(IEnumerable<T> records, IValidator validator);
        Task<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> Update(T record, IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> Update(T record, IValidator validator, IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> UpdateRange(IEnumerable<T> records, IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> UpdateRange(IEnumerable<T> records, IValidator validator, IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier);
        Task<IResponseResult<IApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<IApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers);
        Task<IResponseResult<IApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<IResponseResult<IApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier);
        Task<IResponseResult<IApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<IApiResponse<IEnumerable<TResponse>>>> GetRange<TResponse>(IEnumerable<TIdentifier> identifiers);
        Task<IResponseResult<IApiResponse<IEnumerable<TResponse>>>> GetRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> Search<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
    }
}
