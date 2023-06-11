using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Models.DTOs
{
    public class LoginDTO
    {
        [Required] public string Name { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string AccessToken { get; set; }
    }
}
