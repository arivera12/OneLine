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
        public HttpBaseCrudService(string baseAddress) : base(baseAddress)
        {
        }
        public HttpBaseCrudService(Uri baseAddress) : base(baseAddress)
        {
        }
        public HttpBaseCrudService(string AuthorizationToken, bool AddBearerScheme = true) : base(AuthorizationToken, AddBearerScheme)
        {
        }
        public HttpBaseCrudService(string baseAddress, string AuthorizationToken, bool AddBearerScheme = true) : base(baseAddress, AuthorizationToken, AddBearerScheme)
        {
        }
        public HttpBaseCrudService(Uri baseAddress, string AuthorizationToken, bool AddBearerScheme = true) : base(baseAddress, AuthorizationToken, AddBearerScheme)
        {
        }
        /*Methods without validators*/
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> AddAsync<TResponse>(T record)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", record);
        }
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> AddRangeAsync<TResponse>(IEnumerable<T> records)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddRangeMethod}", records);
        }
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> UpdateAsync<TResponse>(T record)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", record);
        }
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> UpdateRangeAsync<TResponse>(IEnumerable<T> records)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateRangeMethod}", records);
        }
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> DeleteAsync<TResponse>(TIdentifier identifier)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifier);
        }
        public virtual Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifiers);
        }
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> GetOneAsync<TResponse>(TIdentifier identifier)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetOneMethod}", identifier);
        }
        public virtual Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetRangeMethod}", identifiers);
        }
        /*Methods with Validators*/
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> AddAsync<TResponse>(T record, IValidator validator)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", record, validator);
        }
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> AddRangeAsync<TResponse>(IEnumerable<T> records, IValidator validator)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddRangeMethod}", records, validator);
        }
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> UpdateAsync<TResponse>(T record, IValidator validator)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", record, validator);
        }
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> UpdateRangeAsync<TResponse>(IEnumerable<T> records, IValidator validator)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateRangeMethod}", records, validator);
        }
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> DeleteAsync<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifier, validator);
        }
        public virtual Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifiers, validator);
        }
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> GetOneAsync<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetOneMethod}", identifier, validator);
        }
        public virtual Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetRangeMethod}", identifiers, validator); ;
        }
        /*Search method*/
        public virtual Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> SearchAsync<TResponse>(ISearchPaging searchPaging, object searchExtraParams)
        {
            searchPaging ??= new SearchPaging();
            searchExtraParams ??= new { };
            var dictionary = searchPaging.ToDictionary();
            dictionary.Add(new KeyValuePair<string, object>("SearchExtraParams", searchExtraParams));
            return HttpClient.SendJsonResponseResultAsync<Paged<IEnumerable<TResponse>>, object>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{SearchMethod}", dictionary);
        }
    }
}