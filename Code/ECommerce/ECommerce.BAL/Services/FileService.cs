﻿using ECommerce.BAL.Contractors;
using ECommerce.Common.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace ECommerce.BAL.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        public FileService(IWebHostEnvironment environment) => _environment = environment;
        public async Task<string> UploadFileAsync(Guid internalID, string title, IFormFile? file)
        {
            if (file == null || file.Length <= 0)
                return string.Empty;

            if (string.IsNullOrEmpty(_environment.WebRootPath))
                _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), Default.UPLOADS_ROOT);

            var directoryPath = Path.Combine(_environment.WebRootPath, Default.UPLOADS_FOLDER_PATH);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileName = string.Format(Format.UPLOADS_FILE_NAME, internalID, title, Path.GetExtension(file.FileName));
            var filePath = Path.Combine(directoryPath, fileName);
            using (FileStream fs = File.Create(filePath))
            {
                await file.CopyToAsync(fs);
                fs.Flush();
                return fileName;
            }
        }
    }
}
