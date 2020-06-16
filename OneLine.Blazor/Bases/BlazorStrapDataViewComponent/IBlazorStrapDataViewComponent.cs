using OneLine.Bases;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorStrapDataViewComponent<T, TIdentifier, THttpService> :
        IBlazorComponent,
        IDataView<T, TIdentifier, THttpService>
    {
        bool ShowActivityIndicator { get; set; }
        public TColor HighlightItem<TColor>(T record, TColor selectedColor, TColor unSelectedColor);
    }
}