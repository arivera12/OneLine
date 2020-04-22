using FluentValidation;

namespace OneLine.Models.Users
{
    public class LockUnlockValidator : AbstractValidator<ILockUnlock>
    {
        public LockUnlockValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserIdIsRequired")
                .MaximumLength(256)
                .WithMessage("UserIdCanNotBeGreaterThan");
        }
    }

}
