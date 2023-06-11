using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Validator
{
    public class BirthDateValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime date;
            if (value == null)
                return new ValidationResult("The Birth Date field is required.");

            if(!DateTime.TryParse(value.ToString(), out date))
                return new ValidationResult("The Birth Date field must be valid date.");

            if (date >= DateTime.Today)
                return new ValidationResult("The Birth Date field must past date.");

            return ValidationResult.Success;
        }
    }
}
