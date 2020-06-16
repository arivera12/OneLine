using OneLine.Bases;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorFormComponent<T, TIdentifier, THttpService> :
        IBlazorComponent,
        IForm<T, TIdentifier, THttpService>
    {
    }
}
