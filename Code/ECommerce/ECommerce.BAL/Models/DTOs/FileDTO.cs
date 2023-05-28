using Microsoft.AspNetCore.Http;

namespace ECommerce.BAL.Models.DTOs
{
    public class FileDTO
    {
        public IFormFile? File { get; set; }
        public string? StoredFileName { get; set; }
    }
}
