using Microsoft.AspNetCore.Http;

namespace ECommerce.BAL.Contractors
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile? file);
    }
}