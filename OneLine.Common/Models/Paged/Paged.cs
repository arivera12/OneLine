using System;

namespace OneLine.Models
{
    public class Paged<T> : IPaged<T>
    {
        /// <summary>
        /// The page index
        /// </summary>
        public virtual int PageIndex { get; set; }
        /// <summary>
        /// The page size
        /// </summary>

        public virtual int PageSize { get; set; }
        /// <summary>
        /// The total count of records
        /// </summary>

        public virtual int TotalCount { get; set; }
        /// <summary>
        /// The last page is the same as the total of pages. This property is for reference purpose only.
        /// </summary>

        public virtual int LastPage { get; set; }
        /// <summary>
        /// The total pages
        /// </summary>

        public virtual int TotalPages { get; set; }
        /// <summary>
        /// Determines wether you can go back to the previous page of records
        /// </summary>

        public virtual bool HasPreviousPage { get; set; }
        /// <summary>
        /// Determines wether you can go foward to the next page of record
        /// </summary>

        public virtual bool HasNextPage { get; set; }
        /// <summary>
        /// The data
        /// </summary>

        public virtual T Data { get; set; }

        public Paged(int pageIndex, int pageSize, int totalCount, T data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = TotalCount == 0 ? 0 : Convert.ToInt32(Math.Ceiling(TotalCount / Convert.ToDouble(PageSize)));
            LastPage = TotalPages;
            HasPreviousPage = PageIndex > 1;
            HasNextPage = TotalCount != 0 && TotalCount > PageIndex * PageSize;
            Data = data;
        }

        public Paged()
        {
        }
    }
}
