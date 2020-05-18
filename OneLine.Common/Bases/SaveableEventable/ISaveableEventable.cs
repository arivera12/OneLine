using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ISaveableEventable
    {
        Task Save();
        Action OnBeforeSave { get; set; }
        Action OnAfterSave { get; set; }
    }
}
