using FluentValidation;

namespace OneLine.Models
{
    public class BlobDataValidator : AbstractValidator<IBlobData>
    {
        public BlobDataValidator()
        {
            RuleFor(x => x.Data).NotNull().WithMessage("TheBlobDataIsRequired");
            RuleFor(x => x.InputName).NotEmpty().WithMessage("TheInputNameIsRequired");
            RuleFor(x => x.Name).NotEmpty().WithMessage("TheFileNameIsRequired");
        }
    }
}
