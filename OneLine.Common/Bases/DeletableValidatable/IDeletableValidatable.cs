using FluentValidation;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IDeletableValidatable
    {
        Action<Action> OnBeforeDelete { get; set; }
        Task Delete(IValidator validator);
        Action OnAfterDelete { get; set; }
    }
}
