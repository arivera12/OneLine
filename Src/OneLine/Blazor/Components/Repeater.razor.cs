using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace OneLine.Blazor.Components
{
    public class RepeaterModel<TItem> : ComponentBase
    {
        [Parameter] public RenderFragment HeaderTemplate { get; set; }
        [Parameter] public RenderFragment FooterTemplate { get; set; }
        [Parameter] public RenderFragment<TItem> ItemTemplate { get; set; }
        [Parameter] public IEnumerable<TItem> Items { get; set; }
    }
}
