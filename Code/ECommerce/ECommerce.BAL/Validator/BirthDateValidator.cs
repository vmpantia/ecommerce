using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Validator
{
    public class BirthDateValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date;
                if(DateTime.TryParse(value.ToString(), out date))
                    if(date >= DateTime.Today)
                        return new ValidationResult("The Birthdate field must not be a future date.");
                else
                    return new ValidationResult("The Birthdate field must be valid date.");
            }
            return ValidationResult.Success;
        }
    }
}
