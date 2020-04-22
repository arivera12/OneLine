using FluentValidation;

namespace OneLine.Models.Users
{
    class RegisterValidator : AbstractValidator<IRegister>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("UserNameIsRequired")
                .MaximumLength(256)
                .WithMessage("UserNameCanNotBeGreaterThan");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("EmailIsRequired")
                .EmailAddress()
                .WithMessage("EmailFormatNotValid")
                .MaximumLength(256)
                .WithMessage("EmailCanNotBeGreaterThan");
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstNameIsRequired")
                .MaximumLength(50)
                .WithMessage("FirstNameCanNotBeGreaterThan");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastNameIsRequired")
                .MaximumLength(50)
                .WithMessage("LastNameCanNotBeGreaterThan");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("PhoneNumberIsRequired")
                .Matches(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$")
                .WithMessage("PhoneNumberDoesNotSeemsToBeValid")
                .MinimumLength(10)
                .WithMessage("PhoneNumberShouldbeAtLeastTenDigits")
                .MaximumLength(4000)
                .WithMessage("PhoneNumberCanNotBeGreaterThan");
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
