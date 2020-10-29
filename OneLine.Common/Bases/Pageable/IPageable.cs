using OneLine.Models;
using System;

namespace OneLine.Bases
{
    /// <summary>
    /// Defines a pageable class
    /// </summary>
    public interface IPageable
    {
        /// <summary>
        /// The paging model
        /// </summary>
        IPaging Paging { get; set; }
        /// <summary>
        /// Paging changed action callback
        /// </summary>
        Action<IPaging> PagingChanged { get; set; }
    }
}
