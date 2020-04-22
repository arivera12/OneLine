using FluentValidation;

namespace OneLine.Models.Users
{
    public class ForgotPasswordValidator : AbstractValidator<IForgotPassword>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("EmailIsRequired")
                .EmailAddress()
                .WithMessage("EmailFormatNotValid")
                .MaximumLength(256)
                .WithMessage("EmailCanNotBeGreaterThan");
        }
    }

}
