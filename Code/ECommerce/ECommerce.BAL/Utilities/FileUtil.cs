using ECommerce.BAL.Contractors;
using ECommerce.Common.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerce.BAL.Utilities
{
    public class FileUtil : IFileUtil
    {
        private string _resourcesPath;
        public FileUtil(IWebHostEnvironment environment)
        {
            if (string.IsNullOrEmpty(environment.WebRootPath))
                environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), Default.UPLOADS_ROOT);

            var directoryPath = environment.WebRootPath + Default.UPLOADS_FOLDER_PATH;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            _resourcesPath = directoryPath;
        }

        public async Task<string> UploadFileAsync(Guid internalID, string title, IFormFile? file)
        {
            if (file == null || file.Length <= 0)
                return string.Empty;

            var ownDirectory = string.Format(Format.UPLOADS_DIRECTORY, _resourcesPath, internalID);
            if (!Directory.Exists(ownDirectory))
                Directory.CreateDirectory(ownDirectory);

            var fileName = string.Format(Format.UPLOADS_FILE_NAME, Guid.NewGuid(), title, Path.GetExtension(file.FileName));
            var filePath = Path.Combine(ownDirectory, fileName);
            using (FileStream fs = File.Create(filePath))
            {
                await file.CopyToAsync(fs);
                fs.Flush();
                return fileName;
            }
        }

        public string GetURLFilePath(Guid internalID, string? fileName)
        {
            var urlPath = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                var ownDirectory = string.Format(Format.UPLOADS_DIRECTORY, _resourcesPath, internalID);
                var filePath = Path.Combine(ownDirectory, fileName);
                if (File.Exists(filePath))
                    urlPath = Path.Combine(Default.HOST_URL, Default.UPLOADS_URL_FOLDER_PATH, fileName);
            }
            return urlPath;
        }
    }
}
