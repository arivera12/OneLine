using BlazorStrap;
using OneLine.Bases;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorStrapFormComponent<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IBlazorComponent,
        IForm<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
    {
        bool IsFormOpen { get; set; }
        BSModal Modal { get; set; }
    }
}
