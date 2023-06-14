using ECommerce.BAL.Validator;
using ECommerce.DAL.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Models.DTOs
{
    public class UserDTO : User
    {
        [Required] public Guid InternalID { get; set; }
        [Required, MaxLength(15)] public string Username { get; set; }
        [Required, EmailAddress, MaxLength(50)] public string Email { get; set; }
        [Required, MaxLength(100)] public string Password { get; set; }
        [Required, MaxLength(15)] public string Role { get; set; }
        [Display(Name = "First Name"), Required, MaxLength(40)] public string FirstName { get; set; }
        [Display(Name = "Middle Name"), MaxLength(40)] public string? MiddleName { get; set; }
        [Display(Name = "Last Name"), Required, MaxLength(40)] public string LastName { get; set; }
        [Display(Name = "Birth Date"), Required, BirthDateValidator] public string BirthDate { get; set; }

        //Profile
        public string? Profile { get; set; }

        //Common Details
        [Required] public int Status { get; set; }
        public string? StatusDescription { get; set; }
        [Required] public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
