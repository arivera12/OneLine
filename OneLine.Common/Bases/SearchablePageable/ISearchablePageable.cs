using OneLine.Models;
using System;

namespace OneLine.Bases
{
    public interface ISearchablePageable
    {
        ISearchPaging SearchPaging { get; set; }
        Action<ISearchPaging> SearchPagingChanged { get; set; }
    }
}
