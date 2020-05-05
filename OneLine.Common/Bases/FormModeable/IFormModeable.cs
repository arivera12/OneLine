using OneLine.Enums;
using System;

namespace OneLine.Bases
{
    public interface IFormModeable
    {
        FormMode FormMode { get; set; }
        Action<FormMode> FormModeChanged { get; set; }
    }
}
