using Microsoft.Extensions.Configuration;

namespace OneLine.Bases
{
    /// <summary>
    /// The class is configurable and has a configuration holder object
    /// </summary>
    public interface IConfigurable
    {
        /// <summary>
        /// The configuration object
        /// </summary>
        IConfiguration Configuration { get; set; }
    }
}
