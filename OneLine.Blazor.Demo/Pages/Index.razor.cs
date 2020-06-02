using Microsoft.AspNetCore.Components;
using OneLine.Models;
using System.Collections.Generic;

namespace OneLine.Blazor.Demo.Pages
{
    public class IndexBase : ComponentBase
    {
        public IEnumerable<BlobData> BlobDatas { get; set; }
    }
}
