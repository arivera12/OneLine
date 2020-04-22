using FluentValidation;
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

    public interface ISaveableWithValidator
    {
        Task Save(IValidator validator);
        Action<Action> OnBeforeSave { get; set; }
        Action OnAfterSave { get; set; }
    }
}
