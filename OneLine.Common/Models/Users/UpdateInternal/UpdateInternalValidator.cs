using FluentValidation;

namespace OneLine.Models.Users
{
    public class UpdateInternalValidator : AbstractValidator<IUpdateInternal>
    {
        public UpdateInternalValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("UserIdIsRequired")
                .MaximumLength(256)
                .WithMessage("UserIdCanNotBeGreaterThan");
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
        }
    }
}
