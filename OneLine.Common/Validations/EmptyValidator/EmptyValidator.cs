using FluentValidation;
using System.Collections.Generic;

namespace OneLine.Validations
{
    public class EmptyValidator : AbstractValidator<object>
    {
        public EmptyValidator()
        {
        }
    }
    public class EmptyCollectionValidator : AbstractValidator<IEnumerable<object>>
    {
        public EmptyCollectionValidator()
        {
        }
    }
}
