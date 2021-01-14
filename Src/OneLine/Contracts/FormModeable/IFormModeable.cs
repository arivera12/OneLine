using OneLine.Enums;
using System;

namespace OneLine.Contracts
{
    /// <summary>
    /// The class as FormMode single or multiple.
    /// </summary>
    public interface IFormModeable
    {
        /// <summary>
        /// The form mode. Single or multiple.
        /// </summary>
        FormMode FormMode { get; set; }
        /// <summary>
        /// The form mode changed action.
        /// </summary>
        Action<FormMode> FormModeChanged { get; set; }
    }
}
