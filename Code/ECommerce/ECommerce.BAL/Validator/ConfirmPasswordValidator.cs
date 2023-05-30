using ECommerce.BAL.Models.Requests;
using ECommerce.Common.Constants.Messages;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Validator
{
    public class ConfirmPasswordValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var model = (RegisterUserRequest)validationContext.ObjectInstance;
            if (model != null && value != null)
                if (model.Password != (string)value)
                    return new ValidationResult(Error.ATTR_USR_CONFIRM_PASSWORD_NOT_SAME);

            return ValidationResult.Success;
        }
    }
}
