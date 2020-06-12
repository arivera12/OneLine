using FluentValidation;
using OneLine.Models;
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
        Task<ResponseResult<ApiResponse<TResponse>>> Add<TResponse>(T record);
        Task<ResponseResult<ApiResponse<TResponse>>> Add<TResponse>(T record, IValidator validator);
        Task<ResponseResult<ApiResponse<TResponse>>> AddRange<TResponse>(IEnumerable<T> records, IValidator validator);
        Task<ResponseResult<ApiResponse<TResponse>>> AddRange<TResponse>(IEnumerable<T> records);
        Task<ResponseResult<ApiResponse<TResponse>>> Update<TResponse>(T record);
        Task<ResponseResult<ApiResponse<TResponse>>> Update<TResponse>(T record, IValidator validator);
        Task<ResponseResult<ApiResponse<TResponse>>> UpdateRange<TResponse>(IEnumerable<T> records);
        Task<ResponseResult<ApiResponse<TResponse>>> UpdateRange<TResponse>(IEnumerable<T> records, IValidator validator);
        Task<ResponseResult<ApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier);
        Task<ResponseResult<ApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier, IValidator validator);
        Task<ResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers);
        Task<ResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<ResponseResult<ApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier);
        Task<ResponseResult<ApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier, IValidator validator);
        Task<ResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRange<TResponse>(IEnumerable<TIdentifier> identifiers);
        Task<ResponseResult<ApiResponse<IEnumerable<TResponse>>>> GetRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<ResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> Search<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
    }
}
