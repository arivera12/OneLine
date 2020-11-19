using FluentValidation;
using OneLine.Models;
using System.Collections.Generic;

namespace OneLine.Validations
{
    /// <summary>
    /// Identifier string validator
    /// </summary>
    public class IdentifierStringValidator : AbstractValidator<IIdentifier<string>>
    {
        /// <summary>
        /// Default constructor with not empty validation
        /// </summary>
        public IdentifierStringValidator()
        {
            RuleFor(x => x.Model).NotEmpty().WithMessage("TheIdentifierIsRequired");
        }
    }
    /// <summary>
    /// Identifier string collection validator
    /// </summary>
    public class IdentifierStringCollectionValidator : AbstractValidator<IEnumerable<IIdentifier<string>>>
    {
        /// <summary>
        /// Default constructor with not empty validation
        /// </summary>
        public IdentifierStringCollectionValidator()
        {
            RuleForEach(x => x).SetValidator(new IdentifierStringValidator());
        }
    }
}
