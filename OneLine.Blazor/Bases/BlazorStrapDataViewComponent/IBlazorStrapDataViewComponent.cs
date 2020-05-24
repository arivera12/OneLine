using OneLine.Bases;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorStrapDataViewComponent<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IBlazorComponent,
        IDataView<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
    {
        bool ShowActivityIndicator { get; set; }
        public TColor HighlightItem<TColor>(T record, TColor selectedColor, TColor unSelectedColor);
    }
}