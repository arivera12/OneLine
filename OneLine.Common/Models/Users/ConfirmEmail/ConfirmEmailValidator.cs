using FluentValidation;

namespace OneLine.Models.Users
{
    public class ConfirmEmailValidator : AbstractValidator<IConfirmEmail>
    {
        public ConfirmEmailValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserIdIsRequired")
                .MaximumLength(256)
                .WithMessage("UserIdCanNotBeGreaterThan");
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("CodeIsRequired")
                .MaximumLength(256)
                .WithMessage("CodeCanNotBeGreaterThan");
        }
    }
}
