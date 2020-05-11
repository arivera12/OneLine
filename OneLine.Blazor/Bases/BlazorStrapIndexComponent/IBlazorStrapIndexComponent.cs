using OneLine.Bases;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorStrapIndexComponent<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IBlazorComponent,
        IForm<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
    {
        bool IsFormOpen { get; set; }
        bool ShowModal { get; set; }
    }
}
