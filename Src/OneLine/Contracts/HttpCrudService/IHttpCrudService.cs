using FluentValidation;
using OneLine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface IHttpCrudService<T, TIdentifier> : IHttpService
    {
        /// <summary>
        /// The server controller name
        /// </summary>
        string ControllerName { get; set; }
        /// <summary>
        /// The server add method name
        /// </summary>
        string AddMethod { get; set; }
        /// <summary>
        /// The server add range method name
        /// </summary>
        string AddRangeMethod { get; set; }
        /// <summary>
        /// The server update method name
        /// </summary>
        string UpdateMethod { get; set; }
        /// <summary>
        /// The server update range method name
        /// </summary>
        string UpdateRangeMethod { get; set; }
        /// <summary>
        /// The server delete method name
        /// </summary>
        string DeleteMethod { get; set; }
        /// <summary>
        /// The server delete range method name
        /// </summary>
        string DeleteRangeMethod { get; set; }
        /// <summary>
        /// The server get one method name
        /// </summary>
        string GetOneMethod { get; set; }
        /// <summary>
        /// The server get range method name
        /// </summary>
        string GetRangeMethod { get; set; }
        /// <summary>
        /// The server search method name
        /// </summary>
        string SearchMethod { get; set; }
        /// <summary>
        /// The server add method
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<TResponse>>> AddAsync<TResponse>(T record);
        /// <summary>
        /// The server add method with validator
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="record"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<TResponse>>> AddAsync<TResponse>(T record, IValidator validator);
        /// <summary>
        /// The server add range method with validator
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<TResponse>>> AddRangeAsync<TResponse>(IEnumerable<T> records, IValidator validator);
        /// <summary>
        /// The server add range method
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="records"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<TResponse>>> AddRangeAsync<TResponse>(IEnumerable<T> records);
        /// <summary>
        /// The server update method
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<TResponse>>> UpdateAsync<TResponse>(T record);
        /// <summary>
        /// The server update method with validator
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="record"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<TResponse>>> UpdateAsync<TResponse>(T record, IValidator validator);
        /// <summary>
        /// The server update range method
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="records"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<TResponse>>> UpdateRangeAsync<TResponse>(IEnumerable<T> records);
        /// <summary>
        /// The server update range method with validator
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<TResponse>>> UpdateRangeAsync<TResponse>(IEnumerable<T> records, IValidator validator);
        /// <summary>
        /// The server delete method
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<TResponse>>> DeleteAsync<TResponse>(TIdentifier identifier);
        /// <summary>
        /// The server delete method with validator
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="identifier"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<TResponse>>> DeleteAsync<TResponse>(TIdentifier identifier, IValidator validator);
        /// <summary>
        /// The server delete range method
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers);
        /// <summary>
        /// The server delete range method with validator
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="identifiers"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator);
        /// <summary>
        /// The server get one method
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<TResponse>>> GetOneAsync<TResponse>(TIdentifier identifier);
        /// <summary>
        /// The server get one method with validator
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="identifier"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<TResponse>>> GetOneAsync<TResponse>(TIdentifier identifier, IValidator validator);
        /// <summary>
        /// The server get range method
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers);
        /// <summary>
        /// The server get range method with validator
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="identifiers"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator);
        /// <summary>
        /// The server search method
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="SearchPaging"></param>
        /// <param name="searchExtraParams"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> SearchAsync<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
    }
}
