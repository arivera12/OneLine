using Microsoft.Extensions.Configuration;

namespace OneLine.Services
{
    /// <summary>
    /// A service which tells where are static files locate wether in the assembly, remotely or locally.
    /// </summary>
    public interface IApplicationConfigurationSource
    {
        public string ConfigurationFilePath { get; set; }
        public string ResourceFilesBasePath { get; set; }
    }
}
