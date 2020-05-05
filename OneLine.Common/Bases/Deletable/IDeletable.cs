using FluentValidation;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IDeletable
    {
        Action<Action> OnBeforeDelete { get; set; }
        Task Delete();
        Action OnAfterDelete { get; set; }
        Action OnFailedDelete { get; set; }
    }
}
