using FluentValidation;
using OneLine.Contracts;
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
        /// <inheritdoc/>
        public override string Api { get; set; } = "api";
        /// <inheritdoc/>
        public virtual string ControllerName { get; set; }
        /// <inheritdoc/>
        public virtual string AddMethod { get; set; } = "add";
        /// <inheritdoc/>
        public virtual string AddRangeMethod { get; set; } = "addrange";
        /// <inheritdoc/>
        public virtual string UpdateMethod { get; set; } = "update";
        /// <inheritdoc/>
        public virtual string UpdateRangeMethod { get; set; } = "updaterange";
        /// <inheritdoc/>
        public virtual string DeleteMethod { get; set; } = "delete";
        /// <inheritdoc/>
        public virtual string DeleteRangeMethod { get; set; } = "deleterange";
        /// <inheritdoc/>
        public virtual string GetOneMethod { get; set; } = "getone";
        /// <inheritdoc/>
        public virtual string GetRangeMethod { get; set; } = "getrange";
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> AddAsync<TResponse>(T record)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", record);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> AddRangeAsync<TResponse>(IEnumerable<T> records)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddRangeMethod}", records);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> UpdateAsync<TResponse>(T record)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", record);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> UpdateRangeAsync<TResponse>(IEnumerable<T> records)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateRangeMethod}", records);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> DeleteAsync<TResponse>(TIdentifier identifier)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifier);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifiers);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> GetOneAsync<TResponse>(TIdentifier identifier)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetOneMethod}", identifier);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetRangeMethod}", identifiers);
        }
        /*Methods with Validators*/
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> AddAsync<TResponse>(T record, IValidator validator)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", record, validator);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> AddRangeAsync<TResponse>(IEnumerable<T> records, IValidator validator)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddRangeMethod}", records, validator);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> UpdateAsync<TResponse>(T record, IValidator validator)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", record, validator);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> UpdateRangeAsync<TResponse>(IEnumerable<T> records, IValidator validator)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<TResponse, T>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateRangeMethod}", records, validator);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> DeleteAsync<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifier, validator);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifiers, validator);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> GetOneAsync<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetOneMethod}", identifier, validator);
        }
        /// <inheritdoc/>
        public virtual Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetRangeMethod}", identifiers, validator); ;
        }
        /*Search method*/
        /// <inheritdoc/>
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