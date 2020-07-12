using FluentValidation;
using OneLine.Extensions;
using OneLine.Models;
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
    public class HttpBaseCrudService<T, TIdentifier, TId> : HttpBaseService,
        IHttpCrudService<T, TIdentifier>
        where T : class, new()
        where TIdentifier : class, IIdentifier<TId>
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
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> Add<TResponse>(T record)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", record);
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> AddRange<TResponse>(IEnumerable<T> records)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddRangeMethod}", records);
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> Update<TResponse>(T record)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", record);
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> UpdateRange<TResponse>(IEnumerable<T> records)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateRangeMethod}", records);
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifier);
        }
        public virtual async Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifiers);
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetOneMethod}", identifier);
        }
        public virtual async Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRange<TResponse>(IEnumerable<TIdentifier> identifiers)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetRangeMethod}", identifiers);
        }
        /*Methods with Validators*/
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> Add<TResponse>(T record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> AddRange<TResponse>(IEnumerable<T> records, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddRangeMethod}", records, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> Update<TResponse>(T record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> UpdateRange<TResponse>(IEnumerable<T> records, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateRangeMethod}", records, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifier, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifiers, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetOneMethod}", identifier, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetRangeMethod}", identifiers, validator); ;
        }
        /*Search method*/
        public virtual async Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> Search<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            SearchPaging ??= new SearchPaging();
            searchExtraParams ??= new { };
            return await HttpClient.SendJsonResponseResultAsync<Paged<IEnumerable<TResponse>>, object>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{SearchMethod}", new { SearchPaging, searchExtraParams });
        }
    }
}