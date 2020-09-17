using OneLine.Models;
using OneLine.Enums;

namespace OneLine.Extensions
{
    public static class BoolExtensions
    {
        public static ApiResponse<string> TransactionResultApiResponse(this bool value)
        {
            return value ?
                new ApiResponse<string>(ApiResponseStatus.Succeeded) :
                new ApiResponse<string>(ApiResponseStatus.Failed);
        }
        public static ApiResponse<string> TransactionResultApiResponse(this bool value, string message)
        {
            return value ?
                new ApiResponse<string>(ApiResponseStatus.Succeeded, message: message) :
                new ApiResponse<string>(ApiResponseStatus.Failed, message: message);
        }
        public static ApiResponse<TEntity> TransactionResultApiResponse<TEntity>(this bool value, TEntity model)
        {
            return value ?
                new ApiResponse<TEntity>(ApiResponseStatus.Succeeded, model) :
                new ApiResponse<TEntity>(ApiResponseStatus.Failed, model);
        }
        public static ApiResponse<TEntity> TransactionResultApiResponse<TEntity>(this bool value, TEntity model, string message)
        {
            return value ?
                new ApiResponse<TEntity>(ApiResponseStatus.Succeeded, model, message) :
                new ApiResponse<TEntity>(ApiResponseStatus.Failed, model, message);
        }
    }
}

