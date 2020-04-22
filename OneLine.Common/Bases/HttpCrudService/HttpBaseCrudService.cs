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
    public class HttpBaseCrudService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs> : HttpServiceBase,
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
        public virtual string UpdateMethod { get; set; } = "update";
        public virtual string DeleteMethod { get; set; } = "delete";
        public virtual string GetOneMethod { get; set; } = "getone";
        public virtual string SearchMethod { get; set; } = "search";
        public virtual TBlobValidator BlobValidator { get; set; } = new TBlobValidator();
        public HttpBaseCrudService()
        {
            if (!string.IsNullOrWhiteSpace(BaseAddress))
            {
                HttpClient = new HttpClient
                {
                    BaseAddress = new Uri(BaseAddress)
                };
            }
            else
            {
                HttpClient = new HttpClient();
            }
        }
        public HttpBaseCrudService(HttpClient httpClient) : base(httpClient)
        {
            HttpClient = httpClient;
        }
        public HttpBaseCrudService(Uri baseAddress) : base(baseAddress)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = baseAddress
            };
        }
        public HttpBaseCrudService(string AuthorizationToken, bool AddBearerScheme = true) : base(AuthorizationToken, AddBearerScheme)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
            if (!string.IsNullOrWhiteSpace(AuthorizationToken))
            {
                HttpClient.DefaultRequestHeaders.Remove("Authorization");
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{(AddBearerScheme ? "Bearer" : null)} {AuthorizationToken}");
            }
        }
        public HttpBaseCrudService(Uri baseAddress, string AuthorizationToken, bool AddBearerScheme = true) : base(baseAddress, AuthorizationToken, AddBearerScheme)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = baseAddress
            };
            if (!string.IsNullOrWhiteSpace(AuthorizationToken))
            {
                HttpClient.DefaultRequestHeaders.Remove("Authorization");
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{(AddBearerScheme ? "Bearer" : null)} {AuthorizationToken}");
            }
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> Add<TResponse>(T record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Post, $"/{ControllerName}/{AddMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> Add(T record, IValidator validator, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonWithFormDataResponseResultAsync<Tuple<T, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Post, $"/{ControllerName}/{AddMethod}", record, validator, blobDatas, new BlobDataValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> Update<TResponse>(T record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Put, $"/{ControllerName}/{UpdateMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> Update(T record, IValidator validator, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonWithFormDataResponseResultAsync<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>, T, TBlobData>(HttpMethod.Post, $"/{ControllerName}/{UpdateMethod}", record, validator, blobDatas, new BlobDataValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"/{ControllerName}/{DeleteMethod}", identifier, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Get, $"/{ControllerName}/{GetOneMethod}", identifier, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> Search<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<IApiResponse<IPaged<IEnumerable<TResponse>>>>($"/{ControllerName}/{SearchMethod}", new { SearchPaging, searchExtraParams });
        }
    }
}