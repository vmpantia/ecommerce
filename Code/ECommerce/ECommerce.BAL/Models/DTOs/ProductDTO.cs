using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Models.DTOs
{
    public class ProductDTO
    {
        public Guid InternalID { get; set; }
        [MaxLength(30)] public string Name { get; set; }
        [MaxLength(100)] public string? Description { get; set; }

        //Image
        public FileDTO? Image { get; set; }

        //Common Details
        public int Status { get; set; }
        public string? StatusDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
