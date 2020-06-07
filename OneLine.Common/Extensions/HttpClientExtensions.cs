using FluentValidation;
using Newtonsoft.Json;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static class HttpClientExtensions
    {
        #region Add JWT Request Header

        public static void AddJwtAuthorizationBearerHeader(this HttpClient HttpClient, string AuthorizationToken, bool AddBearerScheme = true)
        {
            if (!string.IsNullOrWhiteSpace(AuthorizationToken))
            {
                HttpClient.DefaultRequestHeaders.Remove("Authorization");
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{(AddBearerScheme ? "Bearer" : null)} {AuthorizationToken}");
            }
        }

        #endregion

        #region Send and Receive json data

        public static async Task<T> GetJsonAsync<T>(this HttpClient httpClient, string requestUri)
            => JsonConvert.DeserializeObject<T>(await httpClient.GetStringAsync(requestUri));
        public static async Task<T> GetJsonAsync<T>(this HttpClient httpClient, string requestUri, object queryStringParameters)
            => JsonConvert.DeserializeObject<T>(await httpClient.GetStringAsync($"{requestUri}?{queryStringParameters?.ToUrlQueryString()}"));
        public static async Task<ResponseResult<T>> GetJsonResponseResultAsync<T>(this HttpClient httpClient, string requestUri)
        {
            try
            {
                var response = await httpClient.GetJsonAsync<T>(requestUri);
                return new ResponseResult<T>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex);
            }
        }
        public static async Task<ResponseResult<T>> GetJsonResponseResultAsync<T>(this HttpClient httpClient, string requestUri, object queryStringParameters)
        {
            try
            {
                var response = await httpClient.GetJsonAsync<T>($"{requestUri}?{queryStringParameters?.ToUrlQueryString()}");
                return new ResponseResult<T>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex);
            }
        }
        public static Task PostJsonAsync(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync(HttpMethod.Post, requestUri, content);
        public static Task<T> PostJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Post, requestUri, content);
        public static async Task<ResponseResult<T>> PostJsonResponseResultAsync<T>(this HttpClient httpClient, string requestUri, object content)
        {
            try
            {
                var response = await httpClient.PostJsonAsync<T>(requestUri, content);
                return new ResponseResult<T>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex);
            }
        }
        public static Task PutJsonAsync(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync(HttpMethod.Put, requestUri, content);
        public static Task<T> PutJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Put, requestUri, content);
        public static async Task<ResponseResult<T>> PutJsonResponseResultAsync<T>(this HttpClient httpClient, string requestUri, object content)
        {
            try
            {
                var response = await httpClient.PutJsonAsync<T>(requestUri, content);
                return new ResponseResult<T>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex);
            }
        }
        public static Task DeleteJsonAsync(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync(HttpMethod.Delete, requestUri, content);
        public static Task<T> DeleteJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Delete, requestUri, content);
        public static async Task<ResponseResult<T>> DeleteJsonResponseResultAsync<T>(this HttpClient httpClient, string requestUri, object content)
        {
            try
            {
                var response = await httpClient.DeleteJsonAsync<T>(requestUri, content);
                return new ResponseResult<T>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex);
            }
        }
        public static Task SendJsonAsync(this HttpClient httpClient, HttpMethod method, string requestUri, object content)
            => httpClient.SendJsonAsync<IgnoreResponse>(method, requestUri, content);
        public static async Task<T> SendJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, object content)
        {
            //Get method sends data over the url
            if (method == HttpMethod.Get)
            {
                if (typeof(T) == typeof(IgnoreResponse))
                {
                    return default;
                }
                else
                {
                    return await httpClient.GetJsonAsync<T>(requestUri, content);
                }
            }
            //Anything else goes on the content as json
            else
            {
                var requestJson = JsonConvert.SerializeObject(content);
                var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
                {
                    Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
                });
                if (typeof(T) == typeof(IgnoreResponse))
                {
                    return default;
                }
                else
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseJson);
                }
            }
        }
        public static async Task<ResponseResult<T>> SendJsonResponseResultAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, object content)
        {
            try
            {
                var response = await httpClient.SendJsonAsync<T>(method, requestUri, content);
                return new ResponseResult<T>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex);
            }
        }
        public static async Task<ResponseResult<ApiResponse<TResponse>>> SendJsonResponseResultAsync<TResponse, TContent>(this HttpClient httpClient, HttpMethod method, string requestUri, TContent content)
        {
            return await httpClient.SendJsonResponseResultAsync<ApiResponse<TResponse>>(method, requestUri, content);
        }
        public static async Task<ResponseResult<ApiResponse<TResponse>>> SendJsonResponseResultAsync<TResponse, TContent>(this HttpClient httpClient, HttpMethod method, string requestUri, TContent content, IValidator validator)
        {
            var validationResult = await validator.ValidateAsync(content);
            if (!validationResult.IsValid)
            {
                return new ResponseResult<ApiResponse<TResponse>>
                {
                    Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "ValidationFailed", validationResult.Errors.Select(x => x.ErrorMessage))
                };
            }
            return await httpClient.SendJsonResponseResultAsync<ApiResponse<TResponse>>(method, requestUri, content);
        }
        public static async Task<ResponseResult<ApiResponse<TResponse>>> SendJsonRangeResponseResultAsync<TResponse, TContent>(this HttpClient httpClient, HttpMethod method, string requestUri, IEnumerable<TContent> contents)
        {
            return await httpClient.SendJsonResponseResultAsync<ApiResponse<TResponse>>(method, requestUri, contents);
        }
        public static async Task<ResponseResult<ApiResponse<TResponse>>> SendJsonRangeResponseResultAsync<TResponse, TContent>(this HttpClient httpClient, HttpMethod method, string requestUri, IEnumerable<TContent> contents, IValidator validator)
        {
            if (contents == null || !contents.Any())
            {
                return new ResponseResult<ApiResponse<TResponse>>
                {
                    Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "CollectionIsNullOrEmpty", true)
                };
            }
            foreach (var content in contents)
            {
                var validationResult = await validator.ValidateAsync(content);
                if (!validationResult.IsValid)
                {
                    return new ResponseResult<ApiResponse<TResponse>>
                    {
                        Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "ValidationFailed", validationResult.Errors.Select(x => x.ErrorMessage))
                    };
                }
            }
            return await httpClient.SendJsonResponseResultAsync<ApiResponse<TResponse>>(method, requestUri, contents);
        }
        public static async Task<ResponseResult<ApiResponse<TResponse>>> SendJsonResponseResultAsync<TResponse, TContent, TValidator>(this HttpClient httpClient, HttpMethod method, string requestUri, TContent content)
            where TValidator : IValidator, new()
        {
            TValidator validator = new TValidator();
            var validationResult = await validator.ValidateAsync(content);
            if (!validationResult.IsValid)
            {
                return new ResponseResult<ApiResponse<TResponse>>
                {
                    Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "ValidationFailed", validationResult.Errors.Select(x => x.ErrorMessage))
                };
            }
            return await httpClient.SendJsonResponseResultAsync<ApiResponse<TResponse>>(method, requestUri, content);
        }
        public static async Task<ResponseResult<ApiResponse<TResponse>>> SendJsonRangeResponseResultAsync<TResponse, TContent, TValidator>(this HttpClient httpClient, HttpMethod method, string requestUri, IEnumerable<TContent> contents)
            where TValidator : IValidator, new()
        {
            if (contents == null || !contents.Any())
            {
                return new ResponseResult<ApiResponse<TResponse>>
                {
                    Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "CollectionIsNullOrEmpty", true)
                };
            }
            TValidator validator = new TValidator();
            foreach (var content in contents)
            {
                var validationResult = await validator.ValidateAsync(content);
                if (!validationResult.IsValid)
                {
                    return new ResponseResult<ApiResponse<TResponse>>
                    {
                        Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "ValidationFailed", validationResult.Errors.Select(x => x.ErrorMessage))
                    };
                }
            }
            return await httpClient.SendJsonResponseResultAsync<ApiResponse<TResponse>>(method, requestUri, contents);
        }

        #endregion

        #region Send Multipart Form Data

        public static async Task<T> SendFormDataAsync<T>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, MultipartFormDataContent multipartFormDataContent)
        {
            httpRequestMessage.Content = multipartFormDataContent;
            var serverStrResponse = await httpClient.SendAsync(httpRequestMessage);
            var strResponse = await serverStrResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(strResponse);
        }
        public static async Task<ResponseResult<T>> SendFormDataResponseResultAsync<T>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, MultipartFormDataContent multipartFormDataContent)
        {
            try
            {
                var response = await httpClient.SendFormDataAsync<T>(httpRequestMessage, multipartFormDataContent);
                return new ResponseResult<T>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex);
            }
        }
        public static async Task<ApiResponse<TResponse>> SendBlobDataAsync<TResponse, TBlobData>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TBlobData> blobDatas, IValidator blobValidator)
            where TBlobData : IBlobData
        {
            var multipartFormDataContent = new MultipartFormDataContent();
            if (blobDatas != null && blobDatas.Any())
            {
                foreach (var blob in blobDatas)
                {
                    var blobValidationResult = await blobValidator.ValidateAsync(blob);
                    if (!blobValidationResult.IsValid)
                    {
                        return new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "ValidationFailed", blobValidationResult.Errors.Select(x => x.ErrorMessage));
                    }
                    multipartFormDataContent.Add(new StreamContent(blob.Data), blob.InputName, blob.Name);
                }
            }
            httpRequestMessage.Content = multipartFormDataContent;
            var serverStrResponse = await httpClient.SendAsync(httpRequestMessage);
            var strResponse = await serverStrResponse.Content.ReadAsStringAsync();
            return new ApiResponse<TResponse>(ApiResponseStatus.Succeeded, JsonConvert.DeserializeObject<TResponse>(strResponse));
        }
        public static async Task<ResponseResult<ApiResponse<TResponse>>> SendBlobDataResponseResultAsync<TResponse, TBlobData>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TBlobData> blobDatas, IValidator blobValidator)
            where TBlobData : IBlobData
        {
            try
            {
                var response = await httpClient.SendBlobDataAsync<TResponse, TBlobData>(httpRequestMessage, blobDatas, blobValidator);
                return new ResponseResult<ApiResponse<TResponse>>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ApiResponse<TResponse>>(default, ex);
            }
        }

        #endregion

        #region Send Multipart Form Data With Http Contents

        public static async Task<T> SendHttpContentsAsync<T>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<HttpContent> httpContents)
        {
            var multipartFormDataContent = new MultipartFormDataContent();
            foreach (var httpContent in httpContents)
            {
                multipartFormDataContent.Add(httpContent);
            }
            httpRequestMessage.Content = multipartFormDataContent;
            var serverStrResponse = await httpClient.SendAsync(httpRequestMessage);
            var strResponse = await serverStrResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(strResponse);
        }
        public static async Task<ResponseResult<T>> SendHttpContentsResponseResultAsync<T>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<HttpContent> httpContents)
        {
            try
            {
                var response = await httpClient.SendHttpContentsAsync<T>(httpRequestMessage, httpContents);
                return new ResponseResult<T>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex);
            }
        }

        #endregion

        #region Send Json Content With Multipart Form Data 

        public static async Task<T> SendJsonWithFormDataAsync<T>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content, MultipartFormDataContent multipartFormDataContent)
        {
            //Send content over url
            if (httpRequestMessage.Method == HttpMethod.Get)
            {
                httpRequestMessage.RequestUri = new Uri($"{httpRequestMessage.RequestUri}?{content?.ToUrlQueryString()}");
            }
            //Send content in the MultipartFormData
            else
            {
                var requestJson = JsonConvert.SerializeObject(content);
                var jsonContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                jsonContent.Headers.Remove("Content-Type");
                jsonContent.Headers.Add("Content-Type", "application/json");
                jsonContent.Headers.Add("Content-Disposition", "form-data; name=\"instance\"");
                multipartFormDataContent.Add(jsonContent, "instance");
            }
            httpRequestMessage.Content = multipartFormDataContent;
            var serverStrResponse = await httpClient.SendAsync(httpRequestMessage);
            var strResponse = await serverStrResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(strResponse);
        }
        public static async Task<ResponseResult<T>> SendJsonWithFormDataResponseResultAsync<T>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content, MultipartFormDataContent multipartFormDataContent)
        {
            try
            {
                var response = await httpClient.SendJsonWithFormDataAsync<T>(httpRequestMessage, content, multipartFormDataContent);
                return new ResponseResult<T>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex);
            }
        }
        public static async Task<ResponseResult<ApiResponse<TResponse>>> SendJsonWithFormDataResponseResultAsync<TResponse, TContent, TBlobData>(this HttpClient httpClient, HttpMethod method, string requestUri, TContent content, IEnumerable<TBlobData> blobDatas)
           where TBlobData : IBlobData
        {
            var multipartFormDataContent = new MultipartFormDataContent();
            if (blobDatas != null && blobDatas.Any())
            {
                foreach (var blob in blobDatas)
                {
                    var streamContent = new StreamContent(blob.Data);
                    streamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue(System.Net.Mime.DispositionTypeNames.Attachment)
                    {
                        FileName = blob.Name,
                        Size = blob.Size
                    };
                    multipartFormDataContent.Add(streamContent, blob.InputName, blob.Name);
                }
            }
            return await httpClient.SendJsonWithFormDataResponseResultAsync<ApiResponse<TResponse>>(new HttpRequestMessage(method, requestUri), content, multipartFormDataContent);
        }
        public static async Task<ResponseResult<ApiResponse<TResponse>>> SendJsonWithFormDataResponseResultAsync<TResponse, TContent, TBlobData>(this HttpClient httpClient, HttpMethod method, string requestUri, TContent content, IValidator validator, IEnumerable<TBlobData> blobDatas, IValidator blobValidator)
           where TBlobData : IBlobData
        {
            var validationResult = await validator.ValidateAsync(content);
            if (!validationResult.IsValid)
            {
                return new ResponseResult<ApiResponse<TResponse>>
                {
                    Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "ValidationFailed", validationResult.Errors.Select(x => x.ErrorMessage))
                };
            }
            var multipartFormDataContent = new MultipartFormDataContent();
            if (blobDatas != null && blobDatas.Any())
            {
                foreach (var blob in blobDatas)
                {
                    var blobValidationResult = await blobValidator.ValidateAsync(blob);
                    if (!blobValidationResult.IsValid)
                    {
                        return new ResponseResult<ApiResponse<TResponse>>
                        {
                            Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "ValidationFailed", validationResult.Errors.Select(x => x.ErrorMessage))
                        };
                    }
                    var streamContent = new StreamContent(blob.Data);
                    streamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue(System.Net.Mime.DispositionTypeNames.Attachment)
                    {
                        FileName = blob.Name,
                        Size = blob.Size
                    };
                    multipartFormDataContent.Add(streamContent, blob.InputName, blob.Name);
                }
            }
            return await httpClient.SendJsonWithFormDataResponseResultAsync<ApiResponse<TResponse>>(new HttpRequestMessage(method, requestUri), content, multipartFormDataContent);
        }
        public static async Task<ResponseResult<ApiResponse<TResponse>>> SendJsonRangeWithFormDataResponseResultAsync<TResponse, TContent, TBlobData>(this HttpClient httpClient, HttpMethod method, string requestUri, IEnumerable<TContent> contents, IEnumerable<TBlobData> blobDatas)
          where TBlobData : IBlobData
        {
            var multipartFormDataContent = new MultipartFormDataContent();
            if (blobDatas != null && blobDatas.Any())
            {
                foreach (var blob in blobDatas)
                {
                    var streamContent = new StreamContent(blob.Data);
                    streamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue(System.Net.Mime.DispositionTypeNames.Attachment)
                    {
                        FileName = blob.Name,
                        Size = blob.Size
                    };
                    multipartFormDataContent.Add(streamContent, blob.InputName, blob.Name);
                }
            }
            return await httpClient.SendJsonWithFormDataResponseResultAsync<ApiResponse<TResponse>>(new HttpRequestMessage(method, requestUri), contents, multipartFormDataContent);
        }
        public static async Task<ResponseResult<ApiResponse<TResponse>>> SendJsonRangeWithFormDataResponseResultAsync<TResponse, TContent, TBlobData>(this HttpClient httpClient, HttpMethod method, string requestUri, IEnumerable<TContent> contents, IValidator validator, IEnumerable<TBlobData> blobDatas, IValidator blobValidator)
           where TBlobData : IBlobData
        {
            if (contents == null || !contents.Any())
            {
                return new ResponseResult<ApiResponse<TResponse>>
                {
                    Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "CollectionIsNullOrEmpty", true)
                };
            }
            foreach (var content in contents)
            {
                var validationResult = await validator.ValidateAsync(content);
                if (!validationResult.IsValid)
                {
                    return new ResponseResult<ApiResponse<TResponse>>
                    {
                        Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "ValidationFailed", validationResult.Errors.Select(x => x.ErrorMessage))
                    };
                }
            }
            var multipartFormDataContent = new MultipartFormDataContent();
            if (blobDatas != null && blobDatas.Any())
            {
                foreach (var blob in blobDatas)
                {
                    var blobValidationResult = await blobValidator.ValidateAsync(blob);
                    if (!blobValidationResult.IsValid)
                    {
                        return new ResponseResult<ApiResponse<TResponse>>
                        {
                            Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "ValidationFailed", blobValidationResult.Errors.Select(x => x.ErrorMessage))
                        };
                    }
                    var streamContent = new StreamContent(blob.Data);
                    streamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue(System.Net.Mime.DispositionTypeNames.Attachment)
                    {
                        FileName = blob.Name,
                        Size = blob.Size
                    };
                    multipartFormDataContent.Add(streamContent, blob.InputName, blob.Name);
                }
            }
            return await httpClient.SendJsonWithFormDataResponseResultAsync<ApiResponse<TResponse>>(new HttpRequestMessage(method, requestUri), contents, multipartFormDataContent);
        }
        public static async Task<ResponseResult<ApiResponse<TResponse>>> SendJsonWithFormDataResponseResultAsync<TResponse, TContent, TBlobData, TValidator, TBlobValidator>(this HttpClient httpClient, HttpMethod method, string requestUri, TContent content, IEnumerable<TBlobData> blobDatas)
            where TBlobData : IBlobData
            where TValidator : IValidator, new()
            where TBlobValidator : IValidator, new()
        {
            var validator = new TValidator();
            var validationResult = await validator.ValidateAsync(content);
            if (!validationResult.IsValid)
            {
                return new ResponseResult<ApiResponse<TResponse>>
                {
                    Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "ValidationFailed", validationResult.Errors.Select(x => x.ErrorMessage))
                };
            }
            var multipartFormDataContent = new MultipartFormDataContent();
            if (blobDatas != null && blobDatas.Any())
            {
                var blobValidator = new TBlobValidator();
                foreach (var blob in blobDatas)
                {
                    var blobValidationResult = await blobValidator.ValidateAsync(blob);
                    if (!blobValidationResult.IsValid)
                    {
                        return new ResponseResult<ApiResponse<TResponse>>
                        {
                            Response = new ApiResponse<TResponse>(ApiResponseStatus.Failed, default, "ValidationFailed", blobValidationResult.Errors.Select(x => x.ErrorMessage))
                        };
                    }
                    var streamContent = new StreamContent(blob.Data);
                    streamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue(System.Net.Mime.DispositionTypeNames.Attachment)
                    {
                        FileName = blob.Name,
                        Size = blob.Size
                    };
                    multipartFormDataContent.Add(streamContent, blob.InputName, blob.Name);
                }
            }
            return await httpClient.SendJsonWithFormDataResponseResultAsync<ApiResponse<TResponse>>(new HttpRequestMessage(method, requestUri), content, multipartFormDataContent);
        }

        #endregion

        #region Send Json Content With Http Contents

        public static async Task<T> SendJsonWithHttpContentsAsync<T>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content, IEnumerable<HttpContent> httpContents)
        {
            var multipartFormDataContent = new MultipartFormDataContent();
            //Send content over url
            if (httpRequestMessage.Method == HttpMethod.Get)
            {
                httpRequestMessage.RequestUri = new Uri($"{httpRequestMessage.RequestUri}?{content?.ToUrlQueryString()}");
            }
            //Send content in the MultipartFormData
            else
            {
                var requestJson = JsonConvert.SerializeObject(content);
                var jsonContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                jsonContent.Headers.Remove("Content-Type");
                jsonContent.Headers.Add("Content-Type", "application/json");
                jsonContent.Headers.Add("Content-Disposition", "form-data; name=\"instance\"");
                multipartFormDataContent.Add(jsonContent, "instance");
            }
            foreach (var httpContent in httpContents)
            {
                multipartFormDataContent.Add(httpContent);
            }
            httpRequestMessage.Content = multipartFormDataContent;
            var serverStrResponse = await httpClient.SendAsync(httpRequestMessage);
            var strResponse = await serverStrResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(strResponse);
        }
        public static async Task<ResponseResult<T>> SendJsonWithHttpContentsResponseResultAsync<T>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content, IEnumerable<HttpContent> httpContents)
        {
            try
            {
                var response = await httpClient.SendJsonWithHttpContentsAsync<T>(httpRequestMessage, content, httpContents);
                return new ResponseResult<T>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex);
            }
        }

        #endregion

        #region Download file as byte array

        public static async Task<byte[]> DownloadBlobAsByteArrayAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            var response = await httpClient.SendAsync(httpRequestMessage);
            return await response.Content.ReadAsByteArrayAsync();
        }
        public static async Task<ResponseResult<byte[]>> DownloadBlobAsByteArrayResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            try
            {
                var response = await httpClient.DownloadBlobAsByteArrayAsync(httpRequestMessage);
                return new ResponseResult<byte[]>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<byte[]>(default, ex);
            }
        }
        public static async Task<byte[]> SendJsonDownloadBlobAsByteArrayAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
        {
            //Send content over url
            if (httpRequestMessage.Method == HttpMethod.Get)
            {
                httpRequestMessage.RequestUri = new Uri($"{httpRequestMessage.RequestUri}?{content?.ToUrlQueryString()}");
            }
            //Send content
            else
            {
                var requestJson = JsonConvert.SerializeObject(content);
                httpRequestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            }
            var response = await httpClient.SendAsync(httpRequestMessage);
            return await response.Content.ReadAsByteArrayAsync();
        }
        public static async Task<ResponseResult<byte[]>> SendJsonDownloadBlobAsByteArrayResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
        {
            try
            {
                var response = await httpClient.SendJsonDownloadBlobAsByteArrayAsync(httpRequestMessage, content);
                return new ResponseResult<byte[]>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<byte[]>(default, ex);
            }
        }
        public static async Task<byte[]> SendJsonDownloadBlobAsByteArrayAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
            where TContent : class
        {
            var validationResult = await validator.ValidateAsync(content);
            if (!validationResult.IsValid)
            {
                return null;
            }
            //Send content over url
            if (httpRequestMessage.Method == HttpMethod.Get)
            {
                httpRequestMessage.RequestUri = new Uri($"{httpRequestMessage.RequestUri}?{content?.ToUrlQueryString()}");
            }
            //Send content
            else
            {
                var requestJson = JsonConvert.SerializeObject(content);
                httpRequestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            }
            var response = await httpClient.SendAsync(httpRequestMessage);
            return await response.Content.ReadAsByteArrayAsync();
        }
        public static async Task<ResponseResult<byte[]>> SendJsonDownloadBlobAsByteArrayResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
            where TContent : class
        {
            try
            {
                var response = await httpClient.SendJsonDownloadBlobAsByteArrayAsync(httpRequestMessage, content, validator);
                return new ResponseResult<byte[]>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<byte[]>(default, ex);
            }
        }
        public static async Task<byte[]> SendJsonRangeDownloadBlobAsByteArrayAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TContent> contents, IValidator validator)
            where TContent : class
        {
            if (contents == null || !contents.Any())
            {
                return null;
            }
            foreach (var content in contents)
            {
                var validationResult = await validator.ValidateAsync(content);
                if (!validationResult.IsValid)
                {
                    return null;
                }
            }
            //Send content over url
            if (httpRequestMessage.Method == HttpMethod.Get)
            {
                httpRequestMessage.RequestUri = new Uri($"{httpRequestMessage.RequestUri}?{contents?.ToUrlQueryString()}");
            }
            //Send content
            else
            {
                var requestJson = JsonConvert.SerializeObject(contents);
                httpRequestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            }
            var response = await httpClient.SendAsync(httpRequestMessage);
            return await response.Content.ReadAsByteArrayAsync();
        }
        public static async Task<ResponseResult<byte[]>> SendJsonRangeDownloadBlobAsByteArrayResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TContent> contents, IValidator validator)
            where TContent : class
        {
            try
            {
                var response = await httpClient.SendJsonRangeDownloadBlobAsByteArrayAsync(httpRequestMessage, contents, validator);
                return new ResponseResult<byte[]>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<byte[]>(default, ex);
            }
        }

        #endregion

        #region Download file as stream

        public static async Task<Stream> DownloadBlobAsStreamAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            var response = await httpClient.SendAsync(httpRequestMessage);
            return await response.Content.ReadAsStreamAsync();
        }
        public static async Task<ResponseResult<Stream>> DownloadBlobAsStreamResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            try
            {
                var response = await httpClient.DownloadBlobAsStreamAsync(httpRequestMessage);
                return new ResponseResult<Stream>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<Stream>(default, ex);
            }
        }
        public static async Task<Stream> SendJsonDownloadBlobAsStreamAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
        {
            //Send content over url
            if (httpRequestMessage.Method == HttpMethod.Get)
            {
                httpRequestMessage.RequestUri = new Uri($"{httpRequestMessage.RequestUri}?{content?.ToUrlQueryString()}");
            }
            //Send content
            else
            {
                var requestJson = JsonConvert.SerializeObject(content);
                httpRequestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            }
            var response = await httpClient.SendAsync(httpRequestMessage);
            return await response.Content.ReadAsStreamAsync();
        }
        public static async Task<ResponseResult<Stream>> SendJsonDownloadBlobAsStreamResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
        {
            try
            {
                var response = await httpClient.SendJsonDownloadBlobAsStreamAsync(httpRequestMessage, content);
                return new ResponseResult<Stream>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<Stream>(default, ex);
            }
        }
        public static async Task<Stream> SendJsonDownloadBlobAsStreamAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
            where TContent : class
        {
            if (content == null)
            {
                return null;
            }
            var validationResult = await validator.ValidateAsync(content);
            if (!validationResult.IsValid)
            {
                return null;
            }
            //Send content over url
            if (httpRequestMessage.Method == HttpMethod.Get)
            {
                httpRequestMessage.RequestUri = new Uri($"{httpRequestMessage.RequestUri}?{content?.ToUrlQueryString()}");
            }
            //Send content
            else
            {
                var requestJson = JsonConvert.SerializeObject(content);
                httpRequestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            }
            var response = await httpClient.SendAsync(httpRequestMessage);
            return await response.Content.ReadAsStreamAsync();
        }
        public static async Task<ResponseResult<Stream>> SendJsonDownloadBlobAsStreamResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
            where TContent : class
        {
            try
            {
                var response = await httpClient.SendJsonDownloadBlobAsStreamAsync(httpRequestMessage, content, validator);
                return new ResponseResult<Stream>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<Stream>(default, ex);
            }
        }
        public static async Task<Stream> SendJsonRangeDownloadBlobAsStreamAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TContent> contents, IValidator validator)
            where TContent : class
        {
            if (contents == null || !contents.Any())
            {
                return null;
            }
            foreach (var content in contents)
            {
                var validationResult = await validator.ValidateAsync(content);
                if (!validationResult.IsValid)
                {
                    return null;
                }
            }
            //Send content over url
            if (httpRequestMessage.Method == HttpMethod.Get)
            {
                httpRequestMessage.RequestUri = new Uri($"{httpRequestMessage.RequestUri}?{contents?.ToUrlQueryString()}");
            }
            //Send content
            else
            {
                var requestJson = JsonConvert.SerializeObject(contents);
                httpRequestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            }
            var response = await httpClient.SendAsync(httpRequestMessage);
            return await response.Content.ReadAsStreamAsync();
        }
        public static async Task<ResponseResult<Stream>> SendJsonRangeDownloadBlobAsStreamResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TContent> contents, IValidator validator)
            where TContent : class
        {
            try
            {
                var response = await httpClient.SendJsonRangeDownloadBlobAsStreamAsync(httpRequestMessage, contents, validator);
                return new ResponseResult<Stream>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<Stream>(default, ex);
            }
        }

        #endregion

        #region Download file as byte array and converts to base 64 string

        public static async Task<string> DownloadBlobAsBase64StringAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            var response = await httpClient.SendAsync(httpRequestMessage);
            return Convert.ToBase64String(await response.Content.ReadAsByteArrayAsync());
        }
        public static async Task<ResponseResult<string>> DownloadBlobAsBase64StringResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            try
            {
                var response = await httpClient.DownloadBlobAsBase64StringAsync(httpRequestMessage);
                return new ResponseResult<string>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(default, ex);
            }
        }
        public static async Task<string> SendJsonDownloadBlobAsBase64StringAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
        {
            //Send content over url
            if (httpRequestMessage.Method == HttpMethod.Get)
            {
                httpRequestMessage.RequestUri = new Uri($"{httpRequestMessage.RequestUri}?{content?.ToUrlQueryString()}");
            }
            //Send content
            else
            {
                var requestJson = JsonConvert.SerializeObject(content);
                httpRequestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            }
            var response = await httpClient.SendAsync(httpRequestMessage);
            return Convert.ToBase64String(await response.Content.ReadAsByteArrayAsync());
        }
        public static async Task<ResponseResult<string>> SendJsonDownloadBlobAsBase64StringResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
        {
            try
            {
                var response = await httpClient.SendJsonDownloadBlobAsBase64StringAsync(httpRequestMessage, content);
                return new ResponseResult<string>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(default, ex);
            }
        }

        #endregion

        #region Download base 64 string and convert to byte array

        public static async Task<byte[]> DownloadBase64StringAsByteArrayAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            var response = await httpClient.SendAsync(httpRequestMessage);
            return Convert.FromBase64String(await response.Content.ReadAsStringAsync());
        }
        public static async Task<ResponseResult<byte[]>> DownloadBase64StringAsByteArrayResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            try
            {
                var response = await httpClient.DownloadBase64StringAsByteArrayAsync(httpRequestMessage);
                return new ResponseResult<byte[]>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<byte[]>(default, ex);
            }
        }
        public static async Task<byte[]> SendJsonDownloadBase64StringAsByteArrayAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
        {
            //Send content over url
            if (httpRequestMessage.Method == HttpMethod.Get)
            {
                httpRequestMessage.RequestUri = new Uri($"{httpRequestMessage.RequestUri}?{content?.ToUrlQueryString()}");
            }
            //Send content
            else
            {
                var requestJson = JsonConvert.SerializeObject(content);
                httpRequestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            }
            var response = await httpClient.SendAsync(httpRequestMessage);
            return Convert.FromBase64String(await response.Content.ReadAsStringAsync());
        }
        public static async Task<ResponseResult<byte[]>> SendJsonDownloadBase64StringAsByteArrayResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
        {
            try
            {
                var response = await httpClient.SendJsonDownloadBase64StringAsByteArrayAsync(httpRequestMessage, content);
                return new ResponseResult<byte[]>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<byte[]>(default, ex);
            }
        }

        #endregion

        #region Ignore Response helper class

        class IgnoreResponse { }

        #endregion
    }
}