using FluentValidation;
using System.Collections.Generic;

namespace OneLine.Models.Users
{
    public class AspNetUsersValidator : AbstractValidator<IAspNetUsers>
    {
        public AspNetUsersValidator()
        {
            //RuleFor(x => x.Id).NotEmpty().WithMessage("AspNetUsersIdIsRequired").MaximumLength(450).WithMessage("AspNetUsersIdCanNotBeGreaterThan");
            RuleFor(x => x.UserName).MaximumLength(256).WithMessage("AspNetUsersUserNameCanNotBeGreaterThan");
            RuleFor(x => x.NormalizedUserName).MaximumLength(256).WithMessage("AspNetUsersNormalizedUserNameCanNotBeGreaterThan");
            RuleFor(x => x.Email).MaximumLength(256).WithMessage("AspNetUsersEmailCanNotBeGreaterThan");
            RuleFor(x => x.NormalizedEmail).MaximumLength(256).WithMessage("AspNetUsersNormalizedEmailCanNotBeGreaterThan");
            RuleFor(x => x.EmailConfirmed).NotNull().WithMessage("AspNetUsersEmailConfirmedIsRequired");
            RuleFor(x => x.PasswordHash).MaximumLength(4000).WithMessage("AspNetUsersPasswordHashCanNotBeGreaterThan");
            RuleFor(x => x.SecurityStamp).MaximumLength(4000).WithMessage("AspNetUsersSecurityStampCanNotBeGreaterThan");
            RuleFor(x => x.ConcurrencyStamp).MaximumLength(4000).WithMessage("AspNetUsersConcurrencyStampCanNotBeGreaterThan");
            RuleFor(x => x.PhoneNumber).MaximumLength(4000).WithMessage("AspNetUsersPhoneNumberCanNotBeGreaterThan");
            RuleFor(x => x.PhoneNumberConfirmed).NotNull().WithMessage("AspNetUsersPhoneNumberConfirmedIsRequired");
            RuleFor(x => x.TwoFactorEnabled).NotNull().WithMessage("AspNetUsersTwoFactorEnabledIsRequired");
            RuleFor(x => x.IsLocked).NotNull().WithMessage("AspNetUsersIsLockedIsRequired");
            RuleFor(x => x.LockoutEnabled).NotNull().WithMessage("AspNetUsersLockoutEnabledIsRequired");
            RuleFor(x => x.AccessFailedCount).NotNull().WithMessage("AspNetUsersAccessFailedCountIsRequired");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("AspNetUsersFirstNameIsRequired").MaximumLength(50).WithMessage("AspNetUsersFirstNameCanNotBeGreaterThan");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("AspNetUsersLastNameIsRequired").MaximumLength(50).WithMessage("AspNetUsersLastNameCanNotBeGreaterThan");
        }
    }
    public class AspNetUsersCollectionValidator : AbstractValidator<IEnumerable<IAspNetUsers>>
    {
        public AspNetUsersCollectionValidator()
        {
            RuleForEach(x => x).SetValidator(new AspNetUsersValidator());
        }
    }
}
