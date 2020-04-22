using FluentValidation;

namespace OneLine.Models.Users
{
    public class ResetPasswordInternalValidator : AbstractValidator<IResetPasswordInternal>
    {
        public ResetPasswordInternalValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage("PasswordIsRequired")
               .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d.*)(?=.*\W.*)[a-zA-Z0-9\S]{8,}$")
               .WithMessage("CurrentPasswordShouldContainOneLowerOneUpperOneDigitOneSpecialChardAndEightMinimunLength")
               .MaximumLength(4000)
               .WithMessage("CurrentPasswordCanNotBeGreaterThan");
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
        }
    }
}
