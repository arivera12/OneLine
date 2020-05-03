using OneLine.Models;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ILoadableApiResponseable<T>
    {
        Task Load();
        Action<IResponseResult<IApiResponse<T>>> OnLoad { get; set; }
    }
}
