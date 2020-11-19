using OneLine.Models;
using System;

namespace OneLine.Contracts
{
    /// <summary>
    /// Defines a property which holds a search with paging capabilities
    /// </summary>
    public interface ISearchablePageable
    {
        /// <summary>
        /// The search and paging property
        /// </summary>
        ISearchPaging SearchPaging { get; set; }
        /// <summary>
        /// The search and paging action callback
        /// </summary>
        Action<ISearchPaging> SearchPagingChanged { get; set; }
    }
}
