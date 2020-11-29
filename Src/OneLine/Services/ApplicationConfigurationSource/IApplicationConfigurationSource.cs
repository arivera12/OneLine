using System.Reflection;

namespace OneLine.Services
{
    /// <summary>
    /// A service which tells where are static files locate wether in the assembly.
    /// </summary>
    public interface IApplicationConfigurationSource
    {
        public string ConfigurationFilePath { get; set; }
        public Assembly ConfigurationFileAssemblyFile { get; set; }
        public string ResourceFilesBasePath { get; set; }
        public Assembly ResourceFilesAssemblyFile { get; set; }
    }
}
