using OneLine.Contracts;
using OneLine.Extensions;
using System;
using System.Net.Http;

namespace OneLine.Bases
{
    /// <summary>
    /// This is a base class to be used as an HttpClient Service
    /// </summary>
    public abstract class HttpBaseService : IHttpService
    {
        /// <inheritdoc/>
        public string Api { get; set; } = "api";
        /// <inheritdoc/>
        public HttpClient HttpClient { get; set; }
        public HttpBaseService()
        {
            HttpClient ??= new HttpClient();
        }
        public HttpBaseService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        public HttpBaseService(string baseAddress)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

        }
        public HttpBaseService(Uri baseAddress)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = baseAddress
            };
        }
        public HttpBaseService(string AuthorizationToken, bool AddBearerScheme = true)
        {
            HttpClient = new HttpClient();
            if (!string.IsNullOrWhiteSpace(AuthorizationToken))
            {
                HttpClient.DefaultRequestHeaders.Remove("Authorization");
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{(AddBearerScheme ? "Bearer" : null)} {AuthorizationToken}");
            }
        }
        public HttpBaseService(string baseAddress, string AuthorizationToken, bool AddBearerScheme = true)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
            if (!string.IsNullOrWhiteSpace(AuthorizationToken))
            {
                HttpClient.DefaultRequestHeaders.Remove("Authorization");
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{(AddBearerScheme ? "Bearer" : null)} {AuthorizationToken}");
            }
        }
        public HttpBaseService(Uri baseAddress, string AuthorizationToken, bool AddBearerScheme = true)
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
        /// <summary>
        /// Gets the api path. Example "/Api"
        /// </summary>
        /// <returns></returns>
        public string GetApi()
        {
            return Api.IsNullOrWhiteSpace() ? "" : $"/{Api}";
        }
    }
}