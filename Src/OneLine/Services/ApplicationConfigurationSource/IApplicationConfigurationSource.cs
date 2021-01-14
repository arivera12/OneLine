using System.Reflection;

namespace OneLine.Services
{
    /// <summary>
    /// A service which tells where are static files locate wether in the assembly.
    /// </summary>
    public interface IApplicationConfigurationSource
    {
        public string ConfigurationFilePath { get; }
        public Assembly ConfigurationFileAssemblyFile { get; }
        public string ResourceFilesBasePath { get; }
        public Assembly ResourceFilesAssemblyFile { get; }
    }
}
