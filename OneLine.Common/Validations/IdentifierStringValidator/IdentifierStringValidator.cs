using FluentValidation;
using OneLine.Models;

namespace OneLine.Validations
{
    public class IdentifierStringValidator : AbstractValidator<IIdentifier<string>>
    {
        public IdentifierStringValidator()
        {
            RuleFor(x => x.Model).NotEmpty().WithMessage("TheIdentifierIsRequired");
        }
    }
}
