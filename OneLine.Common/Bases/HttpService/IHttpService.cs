using FluentValidation;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IHttpService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>
    {
        string ControllerName { get; set; }
        string AddMethod { get; set; }
        string UpdateMethod { get; set; }
        string DeleteMethod { get; set; }
        string GetOneMethod { get; set; }
        string SearchMethod { get; set; }
        string BaseAddress { get; set; }
        HttpClient HttpClient { get; set; }
        TBlobValidator BlobValidator { get; set; }
        Task<IResponseResult<IApiResponse<T>>> Add(T record, IValidator validator);
        Task<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> Add(T record, IValidator validator, IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<T>>> Update(T record, IValidator validator);
        Task<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> Update(T record, IValidator validator, IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<T>>> Delete(TIdentifier record, IValidator validator);
        Task<IResponseResult<IApiResponse<T>>> GetOne(TIdentifier record, IValidator validator);
        Task<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> Search(ISearchPaging SearchPaging, object searchExtraParams);
    }
}
