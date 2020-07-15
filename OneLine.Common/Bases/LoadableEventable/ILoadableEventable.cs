using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ILoadableEventable
    {
        Task Load();
        Action OnBeforeLoad { get; set; }
        Action OnAfterLoad { get; set; }
        bool AutoLoad { get; set; }
    }
}
