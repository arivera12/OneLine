using FluentValidation;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Validations;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    /// <summary>
    /// This is a base class to be used as an HttpClient Service with basic crud operations with validators overload methods
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <typeparam name="TIdentifier"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TBlobData"></typeparam>
    /// <typeparam name="TBlobValidator"></typeparam>
    /// <typeparam name="TUserBlobs"></typeparam>
    public class HttpBaseCrudService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs> : HttpBaseService,
        IHttpCrudService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : class, IIdentifier<TId>
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        public override string Api { get; set; } = "api";
        public virtual string ControllerName { get; set; }
        public virtual string AddMethod { get; set; } = "add";
        public virtual string AddRangeMethod { get; set; } = "addrange";
        public virtual string UpdateMethod { get; set; } = "update";
        public virtual string UpdateRangeMethod { get; set; } = "updaterange";
        public virtual string DeleteMethod { get; set; } = "delete";
        public virtual string DeleteRangeMethod { get; set; } = "deleterange";
        public virtual string GetOneMethod { get; set; } = "getone";
        public virtual string GetRangeMethod { get; set; } = "getrange";
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
        /*Methods without validators*/
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> Add<TResponse>(T record)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", record);
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> AddRange<TResponse>(IEnumerable<T> records)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddRangeMethod}", records);
        }
        public virtual async Task<ResponseResult<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> Add(T record, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonWithFormDataResponseResultAsync<Tuple<T, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", record, blobDatas);
        }
        public virtual async Task<ResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> AddRange(IEnumerable<T> records, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonRangeWithFormDataResponseResultAsync<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddRangeMethod}", records, blobDatas);
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> Update<TResponse>(T record)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", record);
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> UpdateRange<TResponse>(IEnumerable<T> records)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateRangeMethod}", records);
        }
        public virtual async Task<ResponseResult<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> Update(T record, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonWithFormDataResponseResultAsync<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", record, blobDatas);
        }
        public virtual async Task<ResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> UpdateRange(IEnumerable<T> records, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonRangeWithFormDataResponseResultAsync<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateRangeMethod}", records, blobDatas);
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifier);
        }
        public virtual async Task<ResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifiers);
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{GetOneMethod}", identifier);
        }
        public virtual async Task<ResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRange<TResponse>(IEnumerable<TIdentifier> identifiers)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{GetRangeMethod}", identifiers);
        }
        /*Methods with Validators*/
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> Add<TResponse>(T record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", record, validator);
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> AddRange<TResponse>(IEnumerable<T> records, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddRangeMethod}", records, validator);
        }
        public virtual async Task<ResponseResult<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> Add(T record, IValidator validator, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonWithFormDataResponseResultAsync<Tuple<T, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", record, validator, blobDatas, new BlobDataValidator());
        }
        public virtual async Task<ResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> AddRange(IEnumerable<T> records, IValidator validator, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonRangeWithFormDataResponseResultAsync<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddRangeMethod}", records, validator, blobDatas, new BlobDataValidator());
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> Update<TResponse>(T record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", record, validator);
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> UpdateRange<TResponse>(IEnumerable<T> records, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateRangeMethod}", records, validator);
        }
        public virtual async Task<ResponseResult<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> Update(T record, IValidator validator, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonWithFormDataResponseResultAsync<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", record, validator, blobDatas, new BlobDataValidator());
        }
        public virtual async Task<ResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> UpdateRange(IEnumerable<T> records, IValidator validator, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonRangeWithFormDataResponseResultAsync<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateRangeMethod}", records, validator, blobDatas, new BlobDataValidator());
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifier, validator);
        }
        public virtual async Task<ResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifiers, validator);
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{GetOneMethod}", identifier, validator);
        }
        public virtual async Task<ResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{GetRangeMethod}", identifiers, validator); ;
        }
        /*Search method*/
        public virtual async Task<ResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> Search<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<ApiResponse<Paged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{SearchMethod}", new[] { SearchPaging, searchExtraParams });
        }
    }
}