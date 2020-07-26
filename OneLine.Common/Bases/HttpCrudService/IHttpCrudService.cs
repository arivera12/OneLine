using FluentValidation;
using OneLine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IHttpCrudService<T, TIdentifier> : IHttpService
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
        Task<IResponseResult<ApiResponse<TResponse>>> AddAsync<TResponse>(T record);
        Task<IResponseResult<ApiResponse<TResponse>>> AddAsync<TResponse>(T record, IValidator validator);
        Task<IResponseResult<ApiResponse<TResponse>>> AddRangeAsync<TResponse>(IEnumerable<T> records, IValidator validator);
        Task<IResponseResult<ApiResponse<TResponse>>> AddRangeAsync<TResponse>(IEnumerable<T> records);
        Task<IResponseResult<ApiResponse<TResponse>>> UpdateAsync<TResponse>(T record);
        Task<IResponseResult<ApiResponse<TResponse>>> UpdateAsync<TResponse>(T record, IValidator validator);
        Task<IResponseResult<ApiResponse<TResponse>>> UpdateRangeAsync<TResponse>(IEnumerable<T> records);
        Task<IResponseResult<ApiResponse<TResponse>>> UpdateRangeAsync<TResponse>(IEnumerable<T> records, IValidator validator);
        Task<IResponseResult<ApiResponse<TResponse>>> DeleteAsync<TResponse>(TIdentifier identifier);
        Task<IResponseResult<ApiResponse<TResponse>>> DeleteAsync<TResponse>(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers);
        Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<IResponseResult<ApiResponse<TResponse>>> GetOneAsync<TResponse>(TIdentifier identifier);
        Task<IResponseResult<ApiResponse<TResponse>>> GetOneAsync<TResponse>(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers);
        Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRangeAsync<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> SearchAsync<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
    }
}
