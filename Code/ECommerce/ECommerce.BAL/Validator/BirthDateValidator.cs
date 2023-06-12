﻿using ECommerce.Common.Constants.Messages;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Validator
{
    public class BirthDateValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime date;
            if (value == null)
                return new ValidationResult(Error.ATTR_USR_BIRTHDATE_REQUIRED);

            if(!DateTime.TryParse(value.ToString(), out date))
                return new ValidationResult(Error.ATTR_USR_BIRTHDATE_VALID_DATE);

            if (date >= DateTime.Today)
                return new ValidationResult(Error.ATTR_USR_BIRTHDATE_PAST_DATE);

            return ValidationResult.Success;
        }
    }
}
