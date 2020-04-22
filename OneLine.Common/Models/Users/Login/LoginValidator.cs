using FluentValidation;

namespace OneLine.Models.Users
{
    public class LoginValidator : AbstractValidator<ILogin>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("UserNameIsRequired")
                .MaximumLength(256)
                .WithMessage("UserNameCanNotBeGreaterThan");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("PasswordIsRequired")
                .MaximumLength(4000)
                .WithMessage("PasswordCanNotBeGreaterThan");
        }
    }
}
