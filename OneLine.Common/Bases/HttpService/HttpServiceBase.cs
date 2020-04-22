using System;
using System.Net.Http;

namespace OneLine.Bases
{
    /// <summary>
    /// This is a base class to be used as an HttpClient Service
    /// </summary>
    public class HttpServiceBase : IHttpService
    {
        public virtual string BaseAddress { get; set; }
        public virtual HttpClient HttpClient { get; set; }
        public HttpServiceBase()
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
    }
}