using System;

namespace OneLine.Bases
{
    public interface ResponseablePageable<T>
    {
        T ResponsePaged { get; set; }
        Action<T> OnResponsePaged { get; set; }
    }
}
