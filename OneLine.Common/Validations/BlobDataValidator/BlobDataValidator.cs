using FluentValidation;
using FluentValidation.Results;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Validations
{
    public class BlobDataValidator : AbstractValidator<BlobData>
    {
        public BlobDataValidator()
        {
            RuleFor(x => x.Data).NotNull().WithMessage("TheBlobDataIsRequired");
            RuleFor(x => x.InputName).NotEmpty().WithMessage("TheInputNameIsRequired");
            RuleFor(x => x.Name).NotEmpty().WithMessage("TheFileNameIsRequired");
            RuleFor(x => x.Size).NotEmpty().WithMessage("TheFileSizeIsRequired");
        }
        /// <summary>
        /// This method runs the internal validator rules and also the form file rules 
        /// and returns a single result set with the validation result.
        /// </summary>
        /// <param name="blobData"></param>
        /// <param name="formFileRules"></param>
        /// <returns></returns>
        public async Task<ValidationResult> ValidateFormFileRulesAsync(BlobData blobData, FormFileRules formFileRules)
        {
            if (formFileRules == null)
            {
                throw new ArgumentNullException("The argument formFileRules is null.");
            }
            if (formFileRules.AllowedBlobMaxLength <= 0)
            {
                throw new ArgumentException("The AllowedBlobMaxLength can't be zero or less.");
            }
            if (formFileRules.AllowedMinimunFiles <= 0)
            {
                throw new ArgumentException("The AllowedMinimunFiles can't be zero or less.");
            }
            if (formFileRules.AllowedMaximunFiles <= 0)
            {
                throw new ArgumentException("The AllowedMaximunFiles can't be zero or less.");
            }            
            var validationFailures = new List<ValidationFailure>();
            if (!formFileRules.IsRequired && (blobData == null || blobData.Size <= 0))
            {
                return new ValidationResult(Enumerable.Empty<ValidationFailure>());
            }
            if (blobData == null || blobData.Size <= 0)
            {
                validationFailures.Add(new ValidationFailure(nameof(blobData.Name), "FileUploadRequired", blobData.Size));
            }
            if (blobData.Size > formFileRules.AllowedBlobMaxLength)
            {
                validationFailures.Add(new ValidationFailure(nameof(blobData.Size), "TheFileHasExceededTheLargestSizeAllowed", blobData.Size));
            }
            if (formFileRules.AllowedExtensions != null && formFileRules.AllowedExtensions.Any() && !formFileRules.AllowedExtensions.Contains(Path.GetExtension(blobData.Name)))
            {
                validationFailures.Add(new ValidationFailure(nameof(blobData.Name), "TheFileDoesNotHaveTheExpectedExtension", blobData.Name));
            }
            var internalValidation = await ValidateAsync(blobData);
            validationFailures.AddRange(internalValidation.Errors);
            return new ValidationResult(validationFailures);
        }
    }
    public class BlobDataCollectionValidator : AbstractValidator<IEnumerable<BlobData>>
    {
        public BlobDataCollectionValidator()
        {
            RuleForEach(x => x).SetValidator(new BlobDataValidator());
        }
        /// <summary>
        /// This method runs the internal validator rules and also the form file rules 
        /// and returns a single result set with the validation result. 
        /// </summary>
        /// <param name="blobDatas"></param>
        /// <param name="formFileRules"></param>
        /// <returns></returns>
        public async Task<ValidationResult> ValidateFormFileRulesAsync(IEnumerable<BlobData> blobDatas, FormFileRules formFileRules)
        {
            if (formFileRules == null)
            {
                throw new ArgumentNullException("The argument formFileRules is null.");
            }
            if (formFileRules.AllowedBlobMaxLength <= 0)
            {
                throw new ArgumentException("The AllowedBlobMaxLength can't be zero or less.");
            }
            if (formFileRules.AllowedMinimunFiles <= 0)
            {
                throw new ArgumentException("The AllowedMinimunFiles can't be zero or less.");
            }
            if (formFileRules.AllowedMaximunFiles <= 0)
            {
                throw new ArgumentException("The AllowedMaximunFiles can't be zero or less.");
            }
            var validationFailures = new List<ValidationFailure>();
            if (!formFileRules.IsRequired && blobDatas.IsNullOrEmpty())
            {
                return new ValidationResult(Enumerable.Empty<ValidationFailure>());
            }
            if(formFileRules.IsRequired && blobDatas.IsNullOrEmpty())
            {
                validationFailures.Add(new ValidationFailure(nameof(blobDatas), "FileUploadRequired", blobDatas));
                return new ValidationResult(validationFailures);
            }
            foreach (var blobData in blobDatas)
            {
                if (!formFileRules.IsRequired && (blobData == null || blobData.Size <= 0))
                {
                    continue;
                }
                else
                {
                    if (blobData.Size <= 0)
                    {
                        validationFailures.Add(new ValidationFailure(nameof(blobData.Name), "FileUploadRequired", blobData.Size));
                    }
                    if (blobData.Size > formFileRules.AllowedBlobMaxLength)
                    {
                        validationFailures.Add(new ValidationFailure(nameof(blobData.Size), "TheFileHasExceededTheLargestSizeAllowed", blobData.Size));
                    }
                    if (formFileRules.AllowedExtensions != null && formFileRules.AllowedExtensions.Any() && !formFileRules.AllowedExtensions.Contains(Path.GetExtension(blobData.Name)))
                    {
                        validationFailures.Add(new ValidationFailure(nameof(blobData.Name), "TheFileDoesNotHaveTheExpectedExtension", blobData.Name));
                    }
                }
            }
            var internalValidation = await ValidateAsync(blobDatas);
            validationFailures.AddRange(internalValidation.Errors);
            return new ValidationResult(validationFailures);
        }
    }
}
