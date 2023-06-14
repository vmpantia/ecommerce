using System.ComponentModel.DataAnnotations;

namespace ECommerce.DAL.DataAccess.Entities
{
    public class User
    {
        [Key] public Guid InternalID { get; set; }
        [MaxLength(15)] public string Username { get; set; }
        [MaxLength(50)] public string Email { get; set; }
        [MaxLength(100)] public string Password { get; set; }
        [MaxLength(15)] public string Role { get; set; }
        [MaxLength(40)] public string FirstName { get; set; }
        [MaxLength(40)] public string? MiddleName { get; set; }
        [MaxLength(40)] public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        //Profile
        public string? Profile { get; set; }

        //Common Details
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
