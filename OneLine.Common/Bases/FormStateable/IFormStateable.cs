using OneLine.Enums;
using System;

namespace OneLine.Bases
{
    public interface IFormStateable
    {
        FormState FormState { get; set; }
        Action<FormState> FormStateChanged { get; set; }
    }
}
