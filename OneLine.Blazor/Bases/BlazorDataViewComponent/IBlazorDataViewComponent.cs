using OneLine.Bases;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorDataViewComponent<T, TIdentifier, THttpService> :
        IBlazorComponent,
        IDataView<T, TIdentifier, THttpService>
    {
    }
}