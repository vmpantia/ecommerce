using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Models.DTOs
{
    public class ProductDTO
    {
        [Required] public Guid InternalID { get; set; }
        [Required, MaxLength(30)] public string Name { get; set; }
        [Required, MaxLength(100)] public string? Description { get; set; }

        //Image
        public string? Image { get; set; }

        //Common Details
        [Required] public int Status { get; set; }
        public string? StatusDescription { get; set; }
        [Required] public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
