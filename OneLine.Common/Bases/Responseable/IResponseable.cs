using OneLine.Models;
using System;

namespace OneLine.Bases
{
    public interface IResponseable<T>
    {
        T Response { get; set; }
        Action<T> OnResponse { get; set; }
        Action<T> OnResponseSucceeded { get; set; }
        Action<T> OnResponseException { get; set; }
        Action<T> OnResponseFailed { get; set; }
    }
}
