using FluentValidation;

namespace OneLine.Models
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
