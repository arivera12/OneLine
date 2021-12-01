using System;

namespace OneLine.Models
{
    /// <summary>
    /// Implements a paged data structure
    /// </summary>
    /// <typeparam name="T">The type of the data</typeparam>
    public class Paged<T> : IPaged<T>
    {
        /// <inheritdoc/>
        public int PageIndex { get; set; }
        /// <inheritdoc/>
        public int PageSize { get; set; }
        /// <inheritdoc/>
        public int TotalCount { get; set; }
        /// <inheritdoc/>
        public int LastPage { get; set; }
        /// <inheritdoc/>
        public int TotalPages { get; set; }
        /// <inheritdoc/>
        public bool HasPreviousPage { get; set; }
        /// <inheritdoc/>
        public bool HasNextPage { get; set; }
        /// <inheritdoc/>
        public T Data { get; set; }
        /// <summary>
        /// The default constructor
        /// </summary>
        public Paged()
        {

        }
        /// <summary>
        /// The paged main constructors
        /// </summary>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="totalCount">The records total count</param>
        /// <param name="data">The paged data</param>
        public Paged(int pageIndex, int pageSize, int totalCount, T data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = TotalCount == 0 ? 0 : Convert.ToInt32(Math.Ceiling(TotalCount / Convert.ToDouble(PageSize)));
            LastPage = TotalPages;
            HasPreviousPage = PageIndex > 0;
            HasNextPage = TotalCount != 0 && PageIndex == 0 ? TotalCount > PageSize : TotalCount > (PageIndex + 1) * PageSize;
            Data = data;
        }
    }
}
