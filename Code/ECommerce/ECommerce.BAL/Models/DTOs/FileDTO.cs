using Microsoft.AspNetCore.Http;

namespace ECommerce.BAL.Models.DTOs
{
    public class FileDTO
    {
        public IFormFile? File { get; set; }
        public string? FileName { get; set; }
        public string? UrlFilePath { get; set; }
    }
}
