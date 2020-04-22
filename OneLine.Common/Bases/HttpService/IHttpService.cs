using System.Net.Http;

namespace OneLine.Bases
{
    public interface IHttpService
    {
        string BaseAddress { get; set; }
        HttpClient HttpClient { get; set; }
    }
}
