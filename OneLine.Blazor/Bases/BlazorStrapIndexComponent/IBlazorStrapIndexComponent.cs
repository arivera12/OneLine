using OneLine.Bases;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorStrapIndexComponent<T, TIdentifier, THttpService> :
        IBlazorComponent,
        IForm<T, TIdentifier, THttpService>
    {
        bool IsFormOpen { get; set; }
        bool ShowModal { get; set; }
    }
}
