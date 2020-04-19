using FluentValidation;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    /// <summary>
    /// This is a base class to be used as an HttpClient Service
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <typeparam name="TValidator"></typeparam>
    /// <typeparam name="TIdentifier"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TIdentifierValidator"></typeparam>
    /// <typeparam name="TSearchExtraParams"></typeparam>
    /// <typeparam name="TBlobData"></typeparam>
    /// <typeparam name="TBlobValidator"></typeparam>
    /// <typeparam name="TUserBlobs"></typeparam>
    public class HttpServiceBase<T, TValidator, TIdentifier, TId, TIdentifierValidator, TSearchExtraParams, TBlobData, TBlobValidator, TUserBlobs> :
        IHttpService<T, TValidator, TIdentifier, TIdentifierValidator, TSearchExtraParams, TBlobData, TBlobValidator, TUserBlobs>
        where T : new()
        where TValidator : IValidator, new()
        where TIdentifier : IIdentifier<TId>
        where TId : class
        where TIdentifierValidator : IValidator, new()
        where TBlobData : IBlobData
        where TBlobValidator : IValidator, new()
        where TUserBlobs : IUserBlobs
    {
        public virtual string ControllerName { get; set; }
        public virtual string AddMethod { get; set; } = "add";
        public virtual string UpdateMethod { get; set; } = "update";
        public virtual string DeleteMethod { get; set; } = "delete";
        public virtual string GetOneMethod { get; set; } = "getone";
        public virtual string SearchMethod { get; set; } = "search";
        public virtual string BaseAddress { get; set; }
        public virtual HttpClient HttpClient { get; set; }
        public virtual TValidator Validator { get; set; } = new TValidator();
        public virtual TIdentifierValidator IdentifierValidator { get; set; } = new TIdentifierValidator();
        public virtual TBlobValidator BlobValidator { get; set; } = new TBlobValidator();
        public HttpServiceBase()
        {
            if(!string.IsNullOrWhiteSpace(BaseAddress))
            {
                HttpClient = new HttpClient
                {
                    BaseAddress = new Uri(BaseAddress)
                };
            }
            else
            {
                HttpClient = new HttpClient();
            }
        }
        public HttpServiceBase(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        public HttpServiceBase(Uri baseAddress)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = baseAddress
            };
        }
        public HttpServiceBase(string AuthorizationToken, bool AddBearerScheme = true)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
            if (!string.IsNullOrWhiteSpace(AuthorizationToken))
            {
                HttpClient.DefaultRequestHeaders.Remove("Authorization");
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{(AddBearerScheme ? "Bearer" : null)} {AuthorizationToken}");
            }
        }
        public HttpServiceBase(Uri baseAddress, string AuthorizationToken, bool AddBearerScheme = true)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = baseAddress
            };
            if (!string.IsNullOrWhiteSpace(AuthorizationToken))
            {
                HttpClient.DefaultRequestHeaders.Remove("Authorization");
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{(AddBearerScheme ? "Bearer" : null)} {AuthorizationToken}");
            }
        }
        public virtual async Task<IResponseResult<IApiResponse<T>>> Add(T record)
        {
            var validationResult = await Validator.ValidateAsync(record);
            if (!validationResult.IsValid)
            {
                return new ResponseResult<IApiResponse<T>>
                {
                    Response = new ApiResponse<T>()
                    {
                        Status = ApiResponseStatus.Failed,
                        Message = validationResult.Errors.FirstOrDefault().ErrorMessage,
                        Data = record,
                        ErrorMessages = validationResult.Errors.Select(x => x.ErrorMessage)
                    }
                };
            }
            return await HttpClient.PostJsonResponseResultAsync<IApiResponse<T>>($"/{ControllerName}/{AddMethod}", record);
        }
        public virtual async Task<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> Add(T record, IEnumerable<TBlobData> blobDatas)
        {
            var validationResult = await Validator.ValidateAsync(record);
            if (!validationResult.IsValid)
            {
                return new ResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>
                {
                    Response = new ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>()
                    {
                        Status = ApiResponseStatus.Failed,
                        Message = validationResult.Errors.FirstOrDefault().ErrorMessage,
                        Data = Tuple.Create(record, Enumerable.Empty<TUserBlobs>()),
                        ErrorMessages = validationResult.Errors.Select(x => x.ErrorMessage)
                    }
                };
            }
            var multipartFormDataContent = new MultipartFormDataContent();
            if (blobDatas != null && blobDatas.Any())
            {
                foreach (var blob in blobDatas)
                {
                    var blobValidationResult = await BlobValidator.ValidateAsync(blob);
                    if (!blobValidationResult.IsValid)
                    {
                        return new ResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>
                        {
                            Response = new ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>()
                            {
                                Status = ApiResponseStatus.Failed,
                                Message = blobValidationResult.Errors.FirstOrDefault().ErrorMessage,
                                Data = Tuple.Create(record, Enumerable.Empty<TUserBlobs>()),
                                ErrorMessages = blobValidationResult.Errors.Select(x => x.ErrorMessage)
                            }
                        };
                    }
                    multipartFormDataContent.Add(new StreamContent(blob.Data), blob.InputName, blob.Name);
                }
            }
            return await HttpClient.SendJsonWithFormDataResponseResultAsync<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>(new HttpRequestMessage(HttpMethod.Post, $"/{ControllerName}/{AddMethod}"), record, multipartFormDataContent);
        }
        public virtual async Task<IResponseResult<IApiResponse<T>>> Update(T record)
        {
            var validationResult = await Validator.ValidateAsync(record);
            if (!validationResult.IsValid)
            {
                return new ResponseResult<IApiResponse<T>>
                {
                    Response = new ApiResponse<T>()
                    {
                        Status = ApiResponseStatus.Failed,
                        Message = validationResult.Errors.FirstOrDefault().ErrorMessage,
                        Data = record,
                        ErrorMessages = validationResult.Errors.Select(x => x.ErrorMessage)
                    }
                };
            }
            return await HttpClient.PutJsonResponseResultAsync<IApiResponse<T>>($"/{ControllerName}/{UpdateMethod}", record);
        }
        public virtual async Task<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> Update(T record, IEnumerable<TBlobData> blobDatas)
        {
            var validationResult = await Validator.ValidateAsync(record);
            if (!validationResult.IsValid)
            {
                return new ResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>
                {
                    Response = new ApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>()
                    {
                        Status = ApiResponseStatus.Failed,
                        Message = validationResult.Errors.FirstOrDefault().ErrorMessage,
                        Data = Tuple.Create(record, Enumerable.Empty<TUserBlobs>(), Enumerable.Empty<TUserBlobs>()),
                        ErrorMessages = validationResult.Errors.Select(x => x.ErrorMessage)
                    }
                };
            }
            var multipartFormDataContent = new MultipartFormDataContent();
            if (blobDatas != null && blobDatas.Any())
            {
                foreach (var blob in blobDatas)
                {
                    var blobValidationResult = await BlobValidator.ValidateAsync(blob);
                    if (!blobValidationResult.IsValid)
                    {
                        return new ResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>
                        {
                            Response = new ApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>()
                            {
                                Status = ApiResponseStatus.Failed,
                                Message = blobValidationResult.Errors.FirstOrDefault().ErrorMessage,
                                Data = Tuple.Create(record, Enumerable.Empty<TUserBlobs>(), Enumerable.Empty<TUserBlobs>()),
                                ErrorMessages = blobValidationResult.Errors.Select(x => x.ErrorMessage)
                            }
                        };
                    }
                    multipartFormDataContent.Add(new StreamContent(blob.Data), blob.InputName, blob.Name);
                }
            }
            return await HttpClient.SendJsonWithFormDataResponseResultAsync<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>(new HttpRequestMessage(HttpMethod.Put, $"/{ControllerName}/{UpdateMethod}"), record, multipartFormDataContent);
        }
        public virtual async Task<IResponseResult<IApiResponse<T>>> Delete(TIdentifier identifier)
        {
            var validationResult = await IdentifierValidator.ValidateAsync(identifier);
            if (!validationResult.IsValid)
            {
                return new ResponseResult<IApiResponse<T>>
                {
                    Response = new ApiResponse<T>()
                    {
                        Status = ApiResponseStatus.Failed,
                        Message = validationResult.Errors.FirstOrDefault().ErrorMessage,
                        Data = new T(),
                        ErrorMessages = validationResult.Errors.Select(x => x.ErrorMessage)
                    }
                };
            }
            return await HttpClient.PutJsonResponseResultAsync<IApiResponse<T>>($"/{ControllerName}/{DeleteMethod}", identifier);
        }
        public virtual async Task<IResponseResult<IApiResponse<T>>> GetOne(TIdentifier identifier)
        {
            var validationResult = await IdentifierValidator.ValidateAsync(identifier);
            if (!validationResult.IsValid)
            {
                return new ResponseResult<IApiResponse<T>>
                {
                    Response = new ApiResponse<T>()
                    {
                        Status = ApiResponseStatus.Failed,
                        Message = validationResult.Errors.FirstOrDefault().ErrorMessage,
                        Data = new T(),
                        ErrorMessages = validationResult.Errors.Select(x => x.ErrorMessage)
                    }
                };
            }
            return await HttpClient.PutJsonResponseResultAsync<IApiResponse<T>>($"/{ControllerName}/{GetOneMethod}", identifier);
        }
        public virtual async Task<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> Search(ISearchPaging SearchPaging, TSearchExtraParams searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<IApiResponse<IPaged<IEnumerable<T>>>>($"/{ControllerName}/{SearchMethod}", new { SearchPaging, searchExtraParams });
        }
    }
}