using BlazorStrap;
using OneLine.Bases;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorStrapFormComponent<T, TIdentifier, THttpService> :
        IBlazorComponent,
        IForm<T, TIdentifier, THttpService>
    {
        bool IsFormOpen { get; set; }
        BSModal Modal { get; set; }
    }
}
