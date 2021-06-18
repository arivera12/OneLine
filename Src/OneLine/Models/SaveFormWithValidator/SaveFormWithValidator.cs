using FluentValidation;
using OneLine.Enums;

namespace OneLine.Models
{
    /// <summary>
    /// Implements a model to save a form.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SaveFormWithValidator<T> : ISaveForm<T>
    {
        /// <inheritdoc/>
        public T Data { get; set; }
        /// <inheritdoc/>
        public IValidator Validator { get; set; }
        /// <inheritdoc/>
        public SaveOperation SaveOperation { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public SaveFormWithValidator()
        {
        }
        /// <summary>
        /// Constructor with the form data
        /// </summary>
        /// <param name="data"></param>
        public SaveFormWithValidator(T data)
        {
            Data = data;
        }
        /// <summary>
        /// Constructor with the form data and the validator
        /// </summary>
        /// <param name="data"></param>
        /// <param name="validator"></param>
        public SaveFormWithValidator(T data, IValidator validator)
        {
            Data = data;
            Validator = validator;
        }
        /// <summary>
        /// Constructor with the form data and the save operation
        /// </summary>
        /// <param name="data"></param>
        /// <param name="saveOperation"></param>
        public SaveFormWithValidator(T data, SaveOperation saveOperation)
        {
            Data = data;
            SaveOperation = saveOperation;
        }
        /// <summary>
        /// Constructor with the form data, validator and save operation
        /// </summary>
        /// <param name="data"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        public SaveFormWithValidator(T data, IValidator validator, SaveOperation saveOperation)
        {
            Data = data;
            Validator = validator;
            SaveOperation = saveOperation;
        }
    }
}
