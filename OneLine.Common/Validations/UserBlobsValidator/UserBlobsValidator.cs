using FluentValidation;
using OneLine.Models;

namespace OneLine.Validations
{
    public class UserBlobsValidator : AbstractValidator<IUserBlobs>
    {
        public UserBlobsValidator()
        {
            //RuleFor(x => x.UserBlobId).NotEmpty().WithMessage("UserBlobsUserBlobIdIsRequired").MaximumLength(128).WithMessage("UserBlobsUserBlobIdCanNotBeGreaterThan");
            RuleFor(x => x.ContentDisposition).MaximumLength(128).WithMessage("UserBlobsContentDispositionCanNotBeGreaterThan");
            RuleFor(x => x.ContentType).MaximumLength(128).WithMessage("UserBlobsContentTypeCanNotBeGreaterThan");
            RuleFor(x => x.FileName).NotEmpty().WithMessage("UserBlobsFileNameIsRequired").MaximumLength(512).WithMessage("UserBlobsFileNameCanNotBeGreaterThan");
            RuleFor(x => x.Name).NotEmpty().WithMessage("UserBlobsNameIsRequired").MaximumLength(128).WithMessage("UserBlobsNameCanNotBeGreaterThan");
            RuleFor(x => x.Length).NotNull().WithMessage("UserBlobsLengthIsRequired");
            RuleFor(x => x.FilePath).NotEmpty().WithMessage("UserBlobsFilePathIsRequired").MaximumLength(512).WithMessage("UserBlobsFilePathCanNotBeGreaterThan");
            RuleFor(x => x.TableName).NotEmpty().WithMessage("UserBlobsTableNameIsRequired").MaximumLength(128).WithMessage("UserBlobsFilePathCanNotBeGreaterThan");
            RuleFor(x => x.IsDeleted).NotNull().WithMessage("UserBlobsIsDeletedIsRequired");
        }
    }
}
