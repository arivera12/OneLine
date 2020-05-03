using OneLine.Models;
using System;

namespace OneLine.Bases
{
    /// <summary>
	/// This interface is a definition of actions based on the state and response result of the api
	/// </summary>
	/// <typeparam name="T">The api response type</typeparam>
    public interface IApiResponseable<T>
    {
        IResponseResult<IApiResponse<T>> Response { get; set; }
        Action<IResponseResult<IApiResponse<T>>> OnResponse { get; set; }
    }
}
