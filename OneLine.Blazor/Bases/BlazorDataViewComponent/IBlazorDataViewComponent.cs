using OneLine.Bases;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorDataViewComponent<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IBlazorComponent,
        IDataView<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
    {
    }
}