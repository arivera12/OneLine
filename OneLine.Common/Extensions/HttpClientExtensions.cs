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

        public static void AddJwtAuthorizationBearerHeader(this HttpClient HttpClient, string AuthorizationToken, bool AddBearerScheme = false)
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
        public static async Task<IResponseResult<T>> GetJsonResponseResultAsync<T>(this HttpClient httpClient, string requestUri)
        {
            try
            {
                var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri));
                if (response.IsNull())
                {
                    return new ResponseResult<T>(default, null, response);
                }
                else
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(stringData))
                    {
                        return new ResponseResult<T>(default, null, response);
                    }
                    else 
                    {
                        return new ResponseResult<T>(JsonConvert.DeserializeObject<T>(stringData), null, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex, null);
            }
        }
        public static async Task<IResponseResult<T>> GetJsonResponseResultAsync<T>(this HttpClient httpClient, string requestUri, object queryStringParameters)
        {
            try
            {
                var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{requestUri}?{queryStringParameters?.ToUrlQueryString()}"));
                if (response.IsNull())
                {
                    return new ResponseResult<T>(default, null, response);
                }
                else
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(stringData))
                    {
                        return new ResponseResult<T>(default, null, response);
                    }
                    else
                    {
                        return new ResponseResult<T>(JsonConvert.DeserializeObject<T>(stringData), null, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex, null);
            }
        }
        public static Task PostJsonAsync(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync(HttpMethod.Post, requestUri, content);
        public static Task<T> PostJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Post, requestUri, content);
        public static async Task<IResponseResult<T>> PostJsonResponseResultAsync<T>(this HttpClient httpClient, string requestUri, object content)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(request);
                if (response.IsNull())
                {
                    return new ResponseResult<T>(default, null, response);
                }
                else
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(stringData))
                    {
                        return new ResponseResult<T>(default, null, response);
                    }
                    else
                    {
                        return new ResponseResult<T>(JsonConvert.DeserializeObject<T>(stringData), null, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex, null);
            }
        }
        public static Task PutJsonAsync(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync(HttpMethod.Put, requestUri, content);
        public static Task<T> PutJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Put, requestUri, content);
        public static async Task<IResponseResult<T>> PutJsonResponseResultAsync<T>(this HttpClient httpClient, string requestUri, object content)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
                request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(request);
                if (response.IsNull())
                {
                    return new ResponseResult<T>(default, null, response);
                }
                else
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(stringData))
                    {
                        return new ResponseResult<T>(default, null, response);
                    }
                    else
                    {
                        return new ResponseResult<T>(JsonConvert.DeserializeObject<T>(stringData), null, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex, null);
            }
        }
        public static Task DeleteJsonAsync(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync(HttpMethod.Delete, requestUri, content);
        public static Task<T> DeleteJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Delete, requestUri, content);
        public static async Task<IResponseResult<T>> DeleteJsonResponseResultAsync<T>(this HttpClient httpClient, string requestUri, object content)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
                request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(request);
                if (response.IsNull())
                {
                    return new ResponseResult<T>(default, null, response);
                }
                else
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(stringData))
                    {
                        return new ResponseResult<T>(default, null, response);
                    }
                    else
                    {
                        return new ResponseResult<T>(JsonConvert.DeserializeObject<T>(stringData), null, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex, null);
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
        public static async Task<IResponseResult<T>> SendJsonResponseResultAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, object content)
        {
            try
            {
                HttpRequestMessage request;
                if (method == HttpMethod.Get)
                {
                    request = new HttpRequestMessage(method, $"{requestUri}?{content?.ToUrlQueryString()}");
                }
                else
                {
                    request = new HttpRequestMessage(method, requestUri);
                    request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                }
                var response = await httpClient.SendAsync(request);
                if (response.IsNull())
                {
                    return new ResponseResult<T>(default, null, response);
                }
                else
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(stringData))
                    {
                        return new ResponseResult<T>(default, null, response);
                    }
                    else
                    {
                        return new ResponseResult<T>(JsonConvert.DeserializeObject<T>(stringData), null, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>(default, ex, null);
            }
        }
        public static async Task<IResponseResult<ApiResponse<TResponse>>> SendJsonResponseResultAsync<TResponse, TContent>(this HttpClient httpClient, HttpMethod method, string requestUri, TContent content)
        {
            return await httpClient.SendJsonResponseResultAsync<ApiResponse<TResponse>>(method, requestUri, content);
        }
        public static async Task<IResponseResult<ApiResponse<TResponse>>> SendJsonResponseResultAsync<TResponse, TContent>(this HttpClient httpClient, HttpMethod method, string requestUri, TContent content, IValidator validator)
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
        public static async Task<IResponseResult<ApiResponse<TResponse>>> SendJsonRangeResponseResultAsync<TResponse, TContent>(this HttpClient httpClient, HttpMethod method, string requestUri, IEnumerable<TContent> contents)
        {
            return await httpClient.SendJsonResponseResultAsync<ApiResponse<TResponse>>(method, requestUri, contents);
        }
        public static async Task<IResponseResult<ApiResponse<TResponse>>> SendJsonRangeResponseResultAsync<TResponse, TContent>(this HttpClient httpClient, HttpMethod method, string requestUri, IEnumerable<TContent> contents, IValidator validator)
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
      
        #endregion

        #region Download file as byte array

        public static async Task<byte[]> DownloadBlobAsByteArrayAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            var response = await httpClient.SendAsync(httpRequestMessage);
            return await response.Content.ReadAsByteArrayAsync();
        }
        public static async Task<IResponseResult<byte[]>> DownloadBlobAsByteArrayResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
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
        public static async Task<IResponseResult<byte[]>> SendJsonDownloadBlobAsByteArrayResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
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
        public static async Task<IResponseResult<byte[]>> SendJsonDownloadBlobAsByteArrayResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
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
        public static async Task<IResponseResult<byte[]>> SendJsonRangeDownloadBlobAsByteArrayResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TContent> contents, IValidator validator)
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

        #region Send request or json and get HttpResponseMessage

        public static async Task<IResponseResult<HttpResponseMessage>> SendRequestResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            try
            {
                var response = await httpClient.SendAsync(httpRequestMessage);
                return new ResponseResult<HttpResponseMessage>(response, null, response);
            }
            catch (Exception ex)
            {
                return new ResponseResult<HttpResponseMessage>(default, ex, default);
            }
        }
        public static async Task<IResponseResult<HttpResponseMessage>> SendJsonRequestResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
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
            try
            {
                var response = await httpClient.SendAsync(httpRequestMessage);
                return new ResponseResult<HttpResponseMessage>(response, null, response);
            }
            catch (Exception ex)
            {
                return new ResponseResult<HttpResponseMessage>(default, ex, default);
            }
        }
        public static async Task<IResponseResult<HttpResponseMessage>> SendJsonRequestResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
            where TContent : class
        {
            var validationResult = await validator.ValidateAsync(content);
            if (!validationResult.IsValid)
            {
                return new ResponseResult<HttpResponseMessage>
                {
                    Response = new HttpResponseMessage(System.Net.HttpStatusCode.NoContent) { ReasonPhrase = "ValidationFailed" }
                };
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
            try
            {
                var response = await httpClient.SendAsync(httpRequestMessage);
                return new ResponseResult<HttpResponseMessage>(response, null, response);
            }
            catch (Exception ex)
            {
                return new ResponseResult<HttpResponseMessage>(default, ex, default);
            }
        }
        public static async Task<HttpResponseMessage> SendJsonAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
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
            try
            {
                return await httpClient.SendAsync(httpRequestMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        #endregion

        #region Ignore Response helper class

        class IgnoreResponse { }

        #endregion
    }
}