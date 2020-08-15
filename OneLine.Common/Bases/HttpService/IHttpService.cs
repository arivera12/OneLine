using System.Net.Http;

namespace OneLine.Bases
{
    public interface IHttpService
    {
        HttpClient HttpClient { get; set; }
    }
}
