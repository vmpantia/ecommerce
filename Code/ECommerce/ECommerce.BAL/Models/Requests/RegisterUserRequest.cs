using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Models.Requests
{
    public class RegisterUserRequest
    {
        [MaxLength(15)] public string Username { get; set; }
        [MaxLength(50)] public string Email { get; set; }
        [MaxLength(100)] public string Password { get; set; }
        [MaxLength(100)] public string ConfirmPassword { get; set; }
    }
}
