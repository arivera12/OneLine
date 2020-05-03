using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ILoadableApiResponseableCollectionable<T>
    {
        Task Load();
        Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnLoadCollection { get; set; }
    }
}
