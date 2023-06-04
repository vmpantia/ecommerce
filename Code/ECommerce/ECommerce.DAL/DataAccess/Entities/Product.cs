using System.ComponentModel.DataAnnotations;

namespace ECommerce.DAL.DataAccess.Entities
{
    public class Product
    {
        [Key] public Guid InternalID { get; set; }
        [MaxLength(30)] public string Name { get; set; }
        [MaxLength(100)] public string? Description { get; set; }

        //Image
        public string? Image { get; set; }


        //Common Details
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
