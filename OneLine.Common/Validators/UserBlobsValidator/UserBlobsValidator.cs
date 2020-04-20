using FluentValidation;
using OneLine.Models;

namespace OneLine.Validators
{
    public class UserBlobsValidator : AbstractValidator<IUserBlobs>
    {
        public UserBlobsValidator()
        {
            //RuleFor(x => x.UserBlobId).NotEmpty().WithMessage("UserBlobsUserBlobIdIsRequired").MaximumLength(128).WithMessage("UserBlobsUserBlobIdCanNotBeGreaterThan");
            RuleFor(x => x.ContentDisposition).MaximumLength(100).WithMessage("UserBlobsContentDispositionCanNotBeGreaterThan");
            RuleFor(x => x.ContentType).MaximumLength(100).WithMessage("UserBlobsContentTypeCanNotBeGreaterThan");
            RuleFor(x => x.FileName).NotEmpty().WithMessage("UserBlobsFileNameIsRequired").MaximumLength(500).WithMessage("UserBlobsFileNameCanNotBeGreaterThan");
            RuleFor(x => x.Name).NotEmpty().WithMessage("UserBlobsNameIsRequired").MaximumLength(100).WithMessage("UserBlobsNameCanNotBeGreaterThan");
            RuleFor(x => x.Length).NotNull().WithMessage("UserBlobsLengthIsRequired");
            RuleFor(x => x.FilePath).NotEmpty().WithMessage("UserBlobsFilePathIsRequired").MaximumLength(500).WithMessage("UserBlobsFilePathCanNotBeGreaterThan");
            RuleFor(x => x.IsDeleted).NotNull().WithMessage("UserBlobsIsDeletedIsRequired");
        }
    }
}
