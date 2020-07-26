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

        #region Send request or json and get byte array

        public static async Task<byte[]> DownloadAsByteArrayAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            var response = await httpClient.SendAsync(httpRequestMessage);
            return await response.Content.ReadAsByteArrayAsync();
        }
        public static async Task<IResponseResult<byte[]>> DownloadAsByteArrayResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            try
            {
                var response = await httpClient.DownloadAsByteArrayAsync(httpRequestMessage);
                return new ResponseResult<byte[]>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<byte[]>(default, ex);
            }
        }
        public static async Task<byte[]> SendJsonDownloadAsByteArrayAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
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
        public static async Task<IResponseResult<byte[]>> SendJsonDownloadAsByteArrayResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
        {
            try
            {
                var response = await httpClient.SendJsonDownloadAsByteArrayAsync(httpRequestMessage, content);
                return new ResponseResult<byte[]>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<byte[]>(default, ex);
            }
        }
        public static async Task<byte[]> SendJsonDownloadAsByteArrayAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
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
        public static async Task<IResponseResult<byte[]>> SendJsonDownloadAsByteArrayResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
            where TContent : class
        {
            try
            {
                var response = await httpClient.SendJsonDownloadAsByteArrayAsync(httpRequestMessage, content, validator);
                return new ResponseResult<byte[]>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<byte[]>(default, ex);
            }
        }
        public static async Task<byte[]> SendJsonRangeDownloadAsByteArrayAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TContent> contents, IValidator validator)
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
        public static async Task<IResponseResult<byte[]>> SendJsonRangeDownloadAsByteArrayResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TContent> contents, IValidator validator)
            where TContent : class
        {
            try
            {
                var response = await httpClient.SendJsonRangeDownloadAsByteArrayAsync(httpRequestMessage, contents, validator);
                return new ResponseResult<byte[]>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<byte[]>(default, ex);
            }
        }

        #endregion

        #region Send request or json and get Stream

        public static async Task<Stream> DownloadAsStreamAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            var response = await httpClient.SendAsync(httpRequestMessage);
            return await response.Content.ReadAsStreamAsync();
        }
        public static async Task<IResponseResult<Stream>> DownloadAsStreamResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            try
            {
                var response = await httpClient.DownloadAsStreamAsync(httpRequestMessage);
                return new ResponseResult<Stream>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<Stream>(default, ex);
            }
        }
        public static async Task<Stream> SendJsonDownloadAsStreamAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
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
        public static async Task<IResponseResult<Stream>> SendJsonDownloadAsStreamResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
        {
            try
            {
                var response = await httpClient.SendJsonDownloadAsStreamAsync(httpRequestMessage, content);
                return new ResponseResult<Stream>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<Stream>(default, ex);
            }
        }
        public static async Task<Stream> SendJsonDownloadAsStreamAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
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
            return await response.Content.ReadAsStreamAsync();
        }
        public static async Task<IResponseResult<Stream>> SendJsonDownloadAsStreamResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
            where TContent : class
        {
            try
            {
                var response = await httpClient.SendJsonDownloadAsStreamAsync(httpRequestMessage, content, validator);
                return new ResponseResult<Stream>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<Stream>(default, ex);
            }
        }
        public static async Task<Stream> SendJsonRangeDownloadAsStreamAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TContent> contents, IValidator validator)
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
        public static async Task<IResponseResult<Stream>> SendJsonRangeDownloadAsStreamResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TContent> contents, IValidator validator)
            where TContent : class
        {
            try
            {
                var response = await httpClient.SendJsonRangeDownloadAsStreamAsync(httpRequestMessage, contents, validator);
                return new ResponseResult<Stream>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<Stream>(default, ex);
            }
        }

        #endregion

        #region Send request or json and get String

        public static async Task<string> DownloadAsStringAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            var response = await httpClient.SendAsync(httpRequestMessage);
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<IResponseResult<string>> DownloadAsStringResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            try
            {
                var response = await httpClient.DownloadAsStringAsync(httpRequestMessage);
                return new ResponseResult<string>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(default, ex);
            }
        }
        public static async Task<string> SendJsonDownloadAsStringAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
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
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<IResponseResult<string>> SendJsonDownloadAsStringResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
        {
            try
            {
                var response = await httpClient.SendJsonDownloadAsStringAsync(httpRequestMessage, content);
                return new ResponseResult<string>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(default, ex);
            }
        }
        public static async Task<string> SendJsonDownloadAsStringAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
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
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<IResponseResult<string>> SendJsonDownloadAsStringResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
            where TContent : class
        {
            try
            {
                var response = await httpClient.SendJsonDownloadAsStringAsync(httpRequestMessage, content, validator);
                return new ResponseResult<string>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(default, ex);
            }
        }
        public static async Task<string> SendJsonRangeDownloadAsStringAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TContent> contents, IValidator validator)
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
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<IResponseResult<string>> SendJsonRangeDownloadAsStringResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, IEnumerable<TContent> contents, IValidator validator)
            where TContent : class
        {
            try
            {
                var response = await httpClient.SendJsonRangeDownloadAsStringAsync(httpRequestMessage, contents, validator);
                return new ResponseResult<string>(response, null);
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(default, ex);
            }
        }

        #endregion

        #region Send request or json and get HttpResponseMessage

        public static async Task<IResponseResult<HttpResponseMessage>> SendRequestHttpResponseMessageResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
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
        public static async Task<IResponseResult<HttpResponseMessage>> SendJsonRequestHttpResponseMessageResponseResultAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
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
        public static async Task<IResponseResult<HttpResponseMessage>> SendJsonRequestHttpResponseMessageResponseResultAsync<TContent>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, TContent content, IValidator validator)
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
        public static async Task<HttpResponseMessage> SendJsonHttpResponseMessageAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object content)
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