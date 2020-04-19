using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ISaveable
    {
        Task Save();
        Action<Action> OnBeforeSave { get; set; }
        Action OnAfterSave { get; set; }
    }
}
