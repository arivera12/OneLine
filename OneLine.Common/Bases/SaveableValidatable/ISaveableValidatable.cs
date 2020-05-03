using FluentValidation;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ISaveableValidatable
    {
        Task Save(IValidator validator);
        Action<Action> OnBeforeSave { get; set; }
        Action OnAfterSave { get; set; }
        Action OnFailedSave { get; set; }
        Action OnFailedValidation { get; set; }
    }
}
