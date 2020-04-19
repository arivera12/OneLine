using Microsoft.Extensions.Configuration;

namespace OneLine.Bases
{
    public interface IConfigurable
    {
        IConfiguration Configuration { get; set; }
    }
}
