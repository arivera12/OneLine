using OneLine.Enums;
using System;

namespace OneLine.Contracts
{
    /// <summary>
    /// The class has a form state
    /// </summary>
    public interface IFormStateable
    {
        /// <summary>
        /// The form state. See <see cref="FormState"/> 
        /// </summary>
        FormState FormState { get; set; }
        /// <summary>
        /// The form state changed action
        /// </summary>
        Action<FormState> FormStateChanged { get; set; }
    }
}
