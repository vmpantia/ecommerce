using ECommerce.BAL.Contractors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerce.BAL.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        public FileService(IWebHostEnvironment environment) => _environment = environment;
        public async Task<string> UploadFileAsync(IFormFile? file)
        {
            if (file == null || file.Length <= 0)
                return string.Empty;

            if (string.IsNullOrEmpty(_environment.WebRootPath))
                _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var directoryPath = _environment.WebRootPath + "\\Resources\\";
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var filePath = directoryPath + file.FileName;
            using (FileStream fs = File.Create(filePath))
            {
                await file.CopyToAsync(fs);
                fs.Flush();
                return filePath;
            }
        }
    }
}
