using FluentValidation;
using OneLine.Models;

namespace OneLine.Validators
{
    public class IdentifierStringValidator<T> : AbstractValidator<T> 
        where T : IIdentifier<string>
    {
        public IdentifierStringValidator()
        {
            RuleFor(x => x.Model).NotEmpty().WithMessage("TheIdentifierIsRequired");
        }
    }
}
