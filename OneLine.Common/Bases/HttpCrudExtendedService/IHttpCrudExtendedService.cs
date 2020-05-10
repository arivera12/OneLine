using FluentValidation;
using OneLine.Models;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs> : IHttpCrudService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>
    {
        string DownloadCsvMethod { get; set; }
        string UploadCsvMethod { get; set; }
        Task<IResponseResult<byte[]>> DownloadCsvAsByteArray(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<Stream>> DownloadCsvAsStream(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<IApiResponse<IEnumerable<T>>>> UploadCsv(IEnumerable<IBlobData> blobDatas, IValidator validator, HttpMethod httpMethod);
    }
}
