using System;
using System.Net.Http;

namespace OneLine.Bases
{
    /// <summary>
    /// This is a base class to be used as an HttpClient Service
    /// </summary>
    public class HttpBaseService : IHttpService
    {
        public virtual string BaseAddress { get; set; }
        public virtual HttpClient HttpClient { get; set; }
        public HttpBaseService()
        {
            if (!string.IsNullOrWhiteSpace(BaseAddress))
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
        public HttpBaseService(HttpClient httpClient)
        {
            HttpClient = httpClient;
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
    }
}