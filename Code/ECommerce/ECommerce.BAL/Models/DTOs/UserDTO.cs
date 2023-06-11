using ECommerce.BAL.Validator;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Models.DTOs
{
    public class UserDTO
    {
        [Required] public Guid InternalID { get; set; }
        [Required, MaxLength(15)] public string Username { get; set; }
        [Required, MaxLength(50), EmailAddress] public string Email { get; set; }
        [Required, MaxLength(100)] public string Password { get; set; }
        [Required, MaxLength(15)] public string Role { get; set; }
        [Required, MaxLength(40)] public string FirstName { get; set; }
        [MaxLength(40)] public string? MiddleName { get; set; }
        [Required, MaxLength(40)] public string LastName { get; set; }
        [Required, BirthDateValidator] public DateTime BirthDate { get; set; }

        //Profile
        public string? Profile { get; set; }

        //Common Details
        [Required] public int Status { get; set; }
        public string? StatusDescription { get; set; }
        [Required] public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
