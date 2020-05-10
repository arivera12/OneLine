using OneLine.Enums;
using OneLine.Models;
using System.Collections.Generic;
using System.Linq;

namespace OneLine.Extensions
{
    public static class IntergerExtensions
    {
        public static bool Succeeded(this int value)
        {
            return value > 0;
        }
        public static IApiResponse<TEntity> TransactionResultApiResponse<TEntity>(this int value, TEntity model)
        {
            var status = value.Succeeded() ? ApiResponseStatus.Succeeded : ApiResponseStatus.Failed;
            return new ApiResponse<TEntity> { Status = status, Data = model };
        }
        public static IApiResponse<IEnumerable<TEntity>> TransactionResultApiResponse<TEntity>(this int value, IEnumerable<TEntity> models)
        {
            var status = value.Succeeded() && value == models.Count() ? ApiResponseStatus.Succeeded : ApiResponseStatus.Failed;
            return new ApiResponse<IEnumerable<TEntity>> { Status = status, Data = models };
        }
        public static IApiResponse<TEntity> TransactionResultApiResponse<TEntity>(this int value, TEntity model, string SuccessMessage, string ErrorMessage)
        {
            var success = value.Succeeded();
            var status = success ? ApiResponseStatus.Succeeded : ApiResponseStatus.Failed;
            var message = success ? SuccessMessage : ErrorMessage;
            return new ApiResponse<TEntity> { Status = status, Data = model, Message = message };
        }
        public static IApiResponse<IEnumerable<TEntity>> TransactionResultApiResponse<TEntity>(this int value, IEnumerable<TEntity> models, string SuccessMessage, string ErrorMessage)
        {
            var success = value.Succeeded();
            var status = success && value == models.Count() ? ApiResponseStatus.Succeeded : ApiResponseStatus.Failed;
            var message = success && value == models.Count() ? SuccessMessage : ErrorMessage;
            return new ApiResponse<IEnumerable<TEntity>> { Status = status, Data = models, Message = message };
        }
    }
}