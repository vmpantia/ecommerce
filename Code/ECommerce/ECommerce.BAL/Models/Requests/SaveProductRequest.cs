using ECommerce.BAL.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace ECommerce.BAL.Models.Requests
{
    public class SaveProductRequest : BaseRequest
    {
        public ProductDTO inputProduct { get; set; }
        public IFormFile? formImage { get; set; }
    }
}
