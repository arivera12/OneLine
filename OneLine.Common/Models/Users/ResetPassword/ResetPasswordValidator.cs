using FluentValidation;

namespace OneLine.Models.Users
{
    public class ResetPasswordValidator : AbstractValidator<IResetPassword>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("EmailIsRequired")
                .EmailAddress()
                .WithMessage("EmailFormatNotValid")
                .MaximumLength(256)
                .WithMessage("EmailCanNotBeGreaterThan");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("PasswordIsRequired")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d.*)(?=.*\W.*)[a-zA-Z0-9\S]{8,}$")
                .WithMessage("PasswordShouldContainOneLowerOneUpperOneDigitOneSpecialChardAndEightMinimunLength")
                .MaximumLength(4000)
                .WithMessage("PasswordCanNotBeGreaterThan");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("ConfirmPasswordAndPasswordAreNotEqual")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d.*)(?=.*\W.*)[a-zA-Z0-9\S]{8,}$")
                .WithMessage("PasswordShouldContainOneLowerOneUpperOneDigitOneSpecialChardAndEightMinimunLength")
                .MaximumLength(4000)
                .WithMessage("PasswordCanNotBeGreaterThan")
                .Equal(x => x.Password)
                .WithMessage("ConfirmPasswordAndPasswordAreNotEqual");
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("CodeIsRequired");
        }
    }
}
