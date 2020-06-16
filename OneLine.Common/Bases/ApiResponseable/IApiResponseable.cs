using OneLine.Models;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    /// <summary>
	/// This interface is a definition of actions based on the state and response result of the api
	/// </summary>
	/// <typeparam name="T">The api response type</typeparam>
    public interface IApiResponseable<T>
    {
        IResponseResult<ApiResponse<T>> Response { get; set; }
        Action<IResponseResult<ApiResponse<T>>> ResponseChanged { get; set; }
    }
}
