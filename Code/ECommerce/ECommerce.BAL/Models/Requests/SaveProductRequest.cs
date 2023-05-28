using ECommerce.BAL.Models.DTOs;

namespace ECommerce.BAL.Models.Requests
{
    public class SaveProductRequest : BaseRequest
    {
        public ProductDTO inputProduct { get; set; }
    }
}
