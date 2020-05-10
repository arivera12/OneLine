using FluentValidation;
using OneLine.Models;
using System.Collections.Generic;

namespace OneLine.Validations
{
    public class IdentifierStringValidator : AbstractValidator<IIdentifier<string>>
    {
        public IdentifierStringValidator()
        {
            RuleFor(x => x.Model).NotEmpty().WithMessage("TheIdentifierIsRequired");
        }
    }
    public class IdentifierStringCollectionValidator : AbstractValidator<IEnumerable<IIdentifier<string>>>
    {
        public IdentifierStringCollectionValidator()
        {
            RuleForEach(x => x).SetValidator(new IdentifierStringValidator());
        }
    }
}
