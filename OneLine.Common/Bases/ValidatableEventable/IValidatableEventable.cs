using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    /// <summary>
    /// Defines methods, properties and action callback to perform validations
    /// </summary>
    public interface IValidatableEventable
    {
        /// <summary>
        /// The context validator with the validation rules
        /// </summary>
        IValidator Validator { get; set; }
        /// <summary>
        /// The validation result
        /// </summary>
        ValidationResult ValidationResult { get; set; }
        /// <summary>
        /// The validation result changed action callback
        /// </summary>
        Action<ValidationResult> ValidationResultChanged { get; set; }
        /// <summary>
        /// Indicator whether the <see cref="ValidationResult"/> succedded or failed
        /// </summary>
        bool IsValidModelState { get; set; }
        /// <summary>
        /// <see cref="IsValidModelState"/> changed action callback
        /// </summary>
        Action<bool> IsValidModelStateChanged { get; set; }
        /// <summary>
        /// Before validation action callback. This action callback should be chained with <see cref="Validate"/>
        /// </summary>
        Action OnBeforeValidate { get; set; }
        /// <summary>
        /// The validate method. This method should be chained with <see cref="OnAfterValidate"/>
        /// </summary>
        /// <returns></returns>
        Task Validate();
        /// <summary>
        /// The afte validate action callback. This method should be called from <see cref="Validate"/>
        /// </summary>
        Action OnAfterValidate { get; set; }
    }
}