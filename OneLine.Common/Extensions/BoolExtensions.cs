using OneLine.Models;

namespace OneLine.Extensions
{
    public static class BoolExtensions
    {
        public static ApiResponse<string> TransactionResult(this bool value)
        {
            return value ?
                new ApiResponse<string> { Status = Enums.ApiResponseStatus.Succeeded } :
                new ApiResponse<string> { Status = Enums.ApiResponseStatus.Failed };
        }

        public static ApiResponse<TEntity> TransactionResult<TEntity>(this bool value, TEntity model)
        {
            return value ?
                new ApiResponse<TEntity> { Status = Enums.ApiResponseStatus.Succeeded, Data = model } :
                new ApiResponse<TEntity> { Status = Enums.ApiResponseStatus.Failed, Data = model };
        }
    }
}

