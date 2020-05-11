using OneLine.Bases;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorFormComponent<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IBlazorComponent,
        IForm<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
    {
    }
}
