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
                new ApiResponse<string> { Status = ApiResponseStatus.Succeeded }.ToJsonActionResult() : 
                new ApiResponse<string> { Status = ApiResponseStatus.Failed }.ToJsonActionResult();
        }
        public static IActionResult OutputTransactionResult<TEntity>(this bool value, TEntity Data)
        {
            return value ?
                new ApiResponse<TEntity> { Status = ApiResponseStatus.Succeeded, Data = Data }.ToJsonActionResult() :
                new ApiResponse<TEntity> { Status = ApiResponseStatus.Failed, Data = Data }.ToJsonActionResult();
        }
        public static IActionResult OutputTransactionResult<TEntity>(this bool value, IEnumerable<TEntity> Data)
        {
            return value ?
                new ApiResponse<IEnumerable<TEntity>> { Status = ApiResponseStatus.Succeeded, Data = Data }.ToJsonActionResult() :
                new ApiResponse<IEnumerable<TEntity>> { Status = ApiResponseStatus.Failed, Data = Data }.ToJsonActionResult();
        }
    }
}

