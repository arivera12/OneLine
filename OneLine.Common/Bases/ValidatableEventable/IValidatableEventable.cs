﻿using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IValidatableEventable
    {
        IValidator Validator { get; set; }
        ValidationResult ValidationResult { get; set; }
        Action<ValidationResult> ValidationResultChanged { get; set; }
        bool IsValidModelState { get; set; }
        Action<bool> IsValidModelStateChanged { get; set; }
        Action OnBeforeValidate { get; set; }
        Task Validate();
        Action OnAfterValidate { get; set; }
    }
}