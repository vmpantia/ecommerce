using Microsoft.AspNetCore.Http;

namespace ECommerce.BAL.Contractors
{
    public interface IFileUtil
    {
        Task<string> UploadFileAsync(Guid internalID, string title, IFormFile? file);
        string GetURLFilePath(Guid internalID, string? fileName);
    }
}