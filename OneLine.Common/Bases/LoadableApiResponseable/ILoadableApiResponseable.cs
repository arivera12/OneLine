using OneLine.Models;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ILoadableApiResponseable<T>
    {
        Task Load();
        Action<IResponseResult<IApiResponse<T>>> OnLoad { get; set; }
        Action<IResponseResult<IApiResponse<T>>> OnLoadSucceeded { get; set; }
        Action<IResponseResult<IApiResponse<T>>> OnLoadException { get; set; }
        Action<IResponseResult<IApiResponse<T>>> OnLoadFailed { get; set; }
    }
}
