﻿using FluentValidation;
using OneLine.Enums;

namespace OneLine.Models
{
    /// <summary>
    /// Defines model to save a form.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISaveFormWithValidator<T> : IDataHolder<T>
    {
        /// <summary>
        /// The validator of the data.
        /// </summary>
        public IValidator Validator { get; set; }
        /// <summary>
        /// The save operation to perform to the form data.
        /// </summary>
        public SaveOperation SaveOperation { get; set; }
    }
}
