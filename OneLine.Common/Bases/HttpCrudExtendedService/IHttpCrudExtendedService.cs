using FluentValidation;
using OneLine.Models;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IHttpCrudExtendedService<T, TIdentifier> : IHttpCrudService<T, TIdentifier>
    {
        string DownloadCsvMethod { get; set; }
        string UploadCsvMethod { get; set; }
        Task<IResponseResult<byte[]>> DownloadCsvAsByteArrayAsync(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<Stream>> DownloadCsvAsStreamAsync(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<HttpResponseMessage>> DownloadCsvAsHttpResponseMessageAsync(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<ApiResponse<IEnumerable<T>>>> UploadCsvAsync(IEnumerable<BlobData> blobDatas, IValidator validator, HttpMethod httpMethod);
    }
}
