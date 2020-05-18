using FluentValidation;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IDeletableEventable
    {
        Task Delete();
        Action OnBeforeDelete { get; set; }
        Action OnAfterDelete { get; set; }
    }
}
