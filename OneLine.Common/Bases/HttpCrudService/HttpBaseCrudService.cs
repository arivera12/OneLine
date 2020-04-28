using FluentValidation;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Validators;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    /// <summary>
    /// This is a base class to be used as an HttpClient Service with basic crud operations
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <typeparam name="TIdentifier"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TBlobData"></typeparam>
    /// <typeparam name="TBlobValidator"></typeparam>
    /// <typeparam name="TUserBlobs"></typeparam>
    public class HttpBaseCrudService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs> : HttpBaseService,
        IHttpCrudService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>
        where T : new()
        where TIdentifier : IIdentifier<TId>
        where TId : class
        where TBlobData : IBlobData
        where TBlobValidator : IValidator, new()
        where TUserBlobs : IUserBlobs
    {
        public virtual string ControllerName { get; set; }
        public virtual string AddMethod { get; set; } = "add";
        public string AddRangeMethod { get; set; } = "addrange";
        public virtual string UpdateMethod { get; set; } = "update";
        public string UpdateRangeMethod { get; set; } = "updaterange";
        public virtual string DeleteMethod { get; set; } = "delete";
        public string DeleteRangeMethod { get; set; } = "deleterange";
        public virtual string GetOneMethod { get; set; } = "getone";
        public string GetRangeMethod { get; set; } = "getrange";
        public virtual string SearchMethod { get; set; } = "search";
        public virtual TBlobValidator BlobValidator { get; set; } = new TBlobValidator();
        public HttpBaseCrudService() : base()
        {
        }
        public HttpBaseCrudService(HttpClient httpClient) : base(httpClient)
        {
        }
        public HttpBaseCrudService(Uri baseAddress) : base(baseAddress)
        {
        }
        public HttpBaseCrudService(string AuthorizationToken, bool AddBearerScheme = true) : base(AuthorizationToken, AddBearerScheme)
        {
        }
        public HttpBaseCrudService(Uri baseAddress, string AuthorizationToken, bool AddBearerScheme = true) : base(baseAddress, AuthorizationToken, AddBearerScheme)
        {
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> Add<TResponse>(T record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Post, $"/{ControllerName}/{AddMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> AddRange<TResponse>(IEnumerable<T> records, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Post, $"/{ControllerName}/{AddRangeMethod}", records, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> Add(T record, IValidator validator, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonWithFormDataResponseResultAsync<Tuple<T, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Post, $"/{ControllerName}/{AddMethod}", record, validator, blobDatas, new BlobDataValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> AddRange(IEnumerable<T> records, IValidator validator, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonRangeWithFormDataResponseResultAsync<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Post, $"/{ControllerName}/{AddRangeMethod}", records, validator, blobDatas, new BlobDataValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> Update<TResponse>(T record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Put, $"/{ControllerName}/{UpdateMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> UpdateRange<TResponse>(IEnumerable<T> records, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Put, $"/{ControllerName}/{UpdateRangeMethod}", records, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> Update(T record, IValidator validator, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonWithFormDataResponseResultAsync<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Put, $"/{ControllerName}/{UpdateMethod}", record, validator, blobDatas, new BlobDataValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> UpdateRange(IEnumerable<T> records, IValidator validator, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonRangeWithFormDataResponseResultAsync<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Put, $"/{ControllerName}/{UpdateRangeMethod}", records, validator, blobDatas, new BlobDataValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"/{ControllerName}/{DeleteMethod}", identifier, validator);
        }
        public async Task<IResponseResult<IApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Delete, $"/{ControllerName}/{DeleteMethod}", identifiers, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Get, $"/{ControllerName}/{GetOneMethod}", identifier, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<IEnumerable<TResponse>>>> GetRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Get, $"/{ControllerName}/{GetRangeMethod}", identifiers, validator); ;
        }
        public virtual async Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> Search<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<IApiResponse<IPaged<IEnumerable<TResponse>>>>($"/{ControllerName}/{SearchMethod}", new { SearchPaging, searchExtraParams });
        }
    }
}