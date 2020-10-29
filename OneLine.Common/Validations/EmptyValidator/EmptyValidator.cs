using FluentValidation;
using System.Collections.Generic;

namespace OneLine.Validations
{
    /// <summary>
    /// Empty validator with no rules
    /// </summary>
    public class EmptyValidator : AbstractValidator<object>
    {
        /// <summary>
        /// Default constructor with no rules
        /// </summary>
        public EmptyValidator()
        {
        }
    }
    /// <summary>
    /// Empty validator with no rules
    /// </summary>
    public class EmptyCollectionValidator : AbstractValidator<IEnumerable<object>>
    {
        /// <summary>
        /// Default constructo with no rules
        /// </summary>
        public EmptyCollectionValidator()
        {
        }
    }
}
