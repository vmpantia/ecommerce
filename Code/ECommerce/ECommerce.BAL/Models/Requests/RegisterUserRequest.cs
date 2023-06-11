using ECommerce.BAL.Models.DTOs;
using ECommerce.BAL.Validator;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Models.Requests
{
    public class RegisterUserRequest
    {
        public UserDTO inputUser { get; set; }
        [Display(Name = "Confirm Password"), Required, MaxLength(100), ConfirmPasswordValidator] public string ConfirmPassword { get; set; }
    }
}
