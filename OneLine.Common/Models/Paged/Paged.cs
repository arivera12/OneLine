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
            TotalPages = totalCount == 0 ? 0 : totalCount / pageSize;
            HasPreviousPage = pageIndex > 1;
            HasNextPage = totalCount == 0 ? false : totalCount > pageIndex * pageSize;
            Data = data;
        }

        public Paged()
        {
        }
    }
}
