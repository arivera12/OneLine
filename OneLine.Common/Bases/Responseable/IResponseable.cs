using OneLine.Models;
using System;

namespace OneLine.Bases
{
    public interface IResponseable<T>
    {
        T Response { get; set; }
        Action<T> OnResponse { get; set; }
    }
}
