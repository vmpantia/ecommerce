using ECommerce.BAL.Contractors;
using ECommerce.Common.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerce.BAL.Services
{
    public class FileService : IFileService
    {
        private string _directoryPath;
        public FileService(IWebHostEnvironment environment)
        {
            if (string.IsNullOrEmpty(environment.WebRootPath))
                environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), Default.UPLOADS_ROOT);

            var directoryPath = environment.WebRootPath + Default.UPLOADS_FOLDER_PATH;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            _directoryPath = directoryPath;
        }

        public async Task<string> UploadFileAsync(Guid internalID, string title, IFormFile? file)
        {
            if (file == null || file.Length <= 0)
                return string.Empty;

            var fileName = string.Format(Format.UPLOADS_FILE_NAME, internalID, title, Path.GetExtension(file.FileName));
            var filePath = Path.Combine(_directoryPath, fileName);
            using (FileStream fs = File.Create(filePath))
            {
                await file.CopyToAsync(fs);
                fs.Flush();
                return fileName;
            }
        }

        public string GetURLFilePath(string? fileName)
        {
            var urlPath = string.Empty;
            if(!string.IsNullOrEmpty(fileName))
            {
                var filePath = Path.Combine(_directoryPath, fileName);
                if (File.Exists(filePath))
                    urlPath = Path.Combine(Default.HOST_URL, Default.UPLOADS_URL_FOLDER_PATH, fileName);
            }
            return urlPath;
        }
    }
}
