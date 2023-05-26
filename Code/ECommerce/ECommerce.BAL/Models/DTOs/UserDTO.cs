using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Models.DTOs
{
    public class UserDTO
    {
        [Key] public Guid InternalID { get; set; }
        [MaxLength(15)] public string Username { get; set; }
        [MaxLength(50)] public string Email { get; set; }
        [MaxLength(100)] public string Password { get; set; }
        [MaxLength(15)] public string Role { get; set; }
        public string? ImagePath { get; set; }

        //Common Details
        public int Status { get; set; }
        public string? StatusDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
