using Microsoft.AspNetCore.Http;

namespace ECommerce.BAL.Contractors
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(Guid internalID, string title, IFormFile? file);
        string GetURLFilePath(string? fileName);
    }
}