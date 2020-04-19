using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ILoadable
    {
        Task Load();
        Action OnLoad { get; set; }
        Action OnLoadSucceeded { get; set; }
        Action OnLoadException { get; set; }
        Action OnLoadFailed { get; set; }
    }
    public interface ILoadable<T>
    {
        Task Load();
        Action<T> OnLoad { get; set; }
        Action<T> OnLoadSucceeded { get; set; }
        Action<T> OnLoadException { get; set; }
        Action<T> OnLoadFailed { get; set; }
    }
}
