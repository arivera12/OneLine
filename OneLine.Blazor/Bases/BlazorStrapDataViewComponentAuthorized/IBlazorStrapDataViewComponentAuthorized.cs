using OneLine.Bases;
using OneLine.Models.Users;
using System.Collections.Generic;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorStrapDataViewComponentAuthorized<T, TIdentifier, THttpService> :
        IBlazorComponent,
        IDataView<T, TIdentifier, THttpService>
    {
        bool ShowActivityIndicator { get; set; }
        public TColor HighlightItem<TColor>(T record, TColor selectedColor, TColor unSelectedColor);
        IEnumerable<string> AuthorizedRoles { get; set; }
        AspNetUsersViewModel User { get; set; }
    }
}