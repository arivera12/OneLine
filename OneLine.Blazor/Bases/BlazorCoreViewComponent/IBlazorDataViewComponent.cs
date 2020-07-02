using OneLine.Bases;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorDataViewComponent<T, TIdentifier, THttpService> :
        IBlazorCoreComponent,
        ICoreView<T, TIdentifier, THttpService>
    {
    }
}