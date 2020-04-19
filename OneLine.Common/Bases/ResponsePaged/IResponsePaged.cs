using System;

namespace OneLine.Bases
{
    public interface IResponsePaged<T>
    {
        T ResponsePaged { get; set; }
        Action<T> OnResponsePaged { get; set; }
        Action<T> OnResponsePagedSucceeded { get; set; }
        Action<T> OnResponsePagedException { get; set; }
        Action<T> OnResponsePagedFailed { get; set; }
    }
}
