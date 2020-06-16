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
        Task<IResponseResult<byte[]>> DownloadCsvAsByteArray(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<HttpResponseMessage>> DownloadCsvAsStream(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<ApiResponse<IEnumerable<T>>>> UploadCsv(IEnumerable<BlobData> blobDatas, IValidator validator, HttpMethod httpMethod);
    }
}
