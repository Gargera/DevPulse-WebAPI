using Application.Interfaces.Services;
using Domain.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;

        public FileStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<ResponseResult<string>> SaveFileAsync(IFormFile file, string folderName)
        {
            string wwwRootPath = _environment.WebRootPath;
            string contentPath = Path.Combine(wwwRootPath, "Images", folderName);

            if (!Directory.Exists(contentPath))
            {
                Directory.CreateDirectory(contentPath);
            }

            string extension = Path.GetExtension(file.FileName).ToLower();
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

            if (!allowedExtensions.Contains(extension))
            {
                return new ResponseResult<string>
                (
                    false,
                    "Only .jpg, .jpeg, .png, and .webp files are allowed.",
                    null
                );
            }

            string uniqueFileName = $"{Guid.NewGuid()}{extension}";
            string fullPath = Path.Combine(contentPath, uniqueFileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return new ResponseResult<string>
            (
                true,
                "Image uploaded successfuly",
                $"/Uploads/{folderName}/{uniqueFileName}"
            );
        }

        public bool DeleteFile(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl)) return false;

            string wwwRootPath = _environment.WebRootPath;

            string cleanedPath = fileUrl.TrimStart('/');
            string fullPath = Path.Combine(wwwRootPath, cleanedPath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }

            return false;
        }
    }
}
