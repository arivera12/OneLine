using Microsoft.AspNetCore.Mvc;
using OneLine.Enums;
using OneLine.Models;
using System.Collections.Generic;

namespace OneLine.Extensions
{
    public static class BoolExtensions
    {
        public static IActionResult OutputTransactionResult(this bool value)
        {
            return value ? 
                new ApiResponse<string> { Status = ApiResponseStatus.Succeeded }.ToJson() : 
                new ApiResponse<string> { Status = ApiResponseStatus.Failed }.ToJson();
        }
        public static IActionResult OutputTransactionResult<TEntity>(this bool value, TEntity Data)
        {
            return value ?
                new ApiResponse<TEntity> { Status = ApiResponseStatus.Succeeded, Data = Data }.ToJson() :
                new ApiResponse<TEntity> { Status = ApiResponseStatus.Failed, Data = Data }.ToJson();
        }
        public static IActionResult OutputTransactionResult<TEntity>(this bool value, IList<TEntity> Data)
        {
            return value ?
                new ApiResponse<IList<TEntity>> { Status = ApiResponseStatus.Succeeded, Data = Data }.ToJson() :
                new ApiResponse<IList<TEntity>> { Status = ApiResponseStatus.Failed, Data = Data }.ToJson();
        }
    }
}

