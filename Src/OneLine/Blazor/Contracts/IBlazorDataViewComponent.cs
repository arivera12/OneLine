using OneLine.Contracts;

namespace OneLine.Blazor.Contracts
{
    public interface IBlazorDataViewComponent<T, TIdentifier, THttpService> :
        IBlazorCoreViewComponent,
        ICoreView<T, TIdentifier, THttpService>
    {
    }
}