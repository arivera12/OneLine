using OneLine.Models;
using System;

namespace OneLine.Bases
{
    public interface IPageable
    {
        IPaging Paging { get; set; }
        Action<IPaging> PagingChanged { get; set; }
    }
}
