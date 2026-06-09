using Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services
{
    public interface IFileStorageService
    {
        public Task<ResponseResult<string>> SaveFileAsync(IFormFile file, string folderName);
        public bool DeleteFile(string fileUrl);
    }
}